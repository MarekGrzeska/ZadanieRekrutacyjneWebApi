using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZadanieRekrutacyjneWebApi.CsvFileModel;
using ZadanieRekrutacyjneWebApi.CsvFileReader;
using ZadanieRekrutacyjneWebApi.DownloadFileService;
using ZadanieRekrutacyjneWebApi.Entities;
using ZadanieRekrutacyjneWebApi.EntityFrameworkDbContext;
using ZadanieRekrutacyjneWebApi.InsertDataService;
using ZadanieRekrutacyjneWebApi.Mappings;
using ZadanieRekrutacyjneWebApi.ProductInfoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EntityFrameworkDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));
builder.Services.AddScoped<IDownloadFileService, DownloadFileService>();
builder.Services.AddScoped<ICsvFileReader, CsvFileReader>();
builder.Services.AddScoped<InsertDataService>();
builder.Services.AddScoped<ProductInfoService>();
builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    var scoped = provider.CreateScope();
    cfg.AddProfile(new MappingProfile());
}).CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

const string PRODUCTS_URL = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv";
const string INVENTORY_URL = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv";
const string PRICES_URL = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv";

const string PRODUCTS_SAVE_FILENAME = "products.csv";
const string INVENTORY_SAVE_FILENAME = "inventory.csv";
const string PRICES_SAVE_FILENAME = "prices.csv";

var scope = app.Services.CreateScope();
var downloadFileService = scope.ServiceProvider.GetService<IDownloadFileService>();
var csvFileReader = scope.ServiceProvider.GetService<ICsvFileReader>();
var mapper = scope.ServiceProvider.GetService<IMapper>();
var insertDataService = scope.ServiceProvider.GetService<InsertDataService>();
var productInfoService = scope.ServiceProvider.GetRequiredService<ProductInfoService>();

app.MapPost("DownloadDataAndInitDatabase", async () =>
{
    // pobieranie plików
    await downloadFileService.DownloadAndSaveFile(PRODUCTS_URL, PRODUCTS_SAVE_FILENAME);
    await downloadFileService.DownloadAndSaveFile(INVENTORY_URL, INVENTORY_SAVE_FILENAME);
    await downloadFileService.DownloadAndSaveFile(PRICES_URL, PRICES_SAVE_FILENAME);

    // odczyt z plików csv
    var productsList = csvFileReader.ReadCsvFileToList<ProductCsv>(PRODUCTS_SAVE_FILENAME, ";");
    var inventoryList = csvFileReader.ReadCsvFileToList<InventoryCsv>(INVENTORY_SAVE_FILENAME, ",");
    var pricesList = csvFileReader.ReadCsvFileToList<PriceCsv>(PRICES_SAVE_FILENAME, ",", false);

    // przetworzenie danych z wytycznymi przed zapisem do bazy
    var selectedProductList = productsList
    .Where(p => !p.is_wire && (p.shipping == "24h" || p.shipping == "Wysy³ka w 24h")).ToList();

    var selectedInventoryList = inventoryList
    .Where(i => i.shipping == "24h" || i.shipping == "Wysy³ka w 24h").ToList();

    var productsEntityList = mapper.Map<List<Product>>(selectedProductList);
    var inventoriesEntityList = mapper.Map<List<Inventory>>(selectedInventoryList);
    var pricesEntityList = mapper.Map<List<Price>>(pricesList);

    // zapis do bazy
    await insertDataService.InsertProducts(productsEntityList);
    await insertDataService.InsertInventories(inventoriesEntityList);
    await insertDataService.InsertPrices(pricesEntityList);

    return "Pobrano pliki i zapisano dane do bazy";
});

app.MapGet("GetBySku", async (string sku) =>
{
    var dto =  await productInfoService.GetBySku(sku);
    return new
    {
        NazwaProduktu = dto.Name,
        EAN = dto.EAN,
        NazwaProducenta = dto.Producer_name,
        Kategoria = dto.Category,
        UrlDoZdjeciaProduktu = dto.Default_image,
        StanMagazynowy = dto.qty,
        JednostaLogistyczna = dto.unit,
        CenaNettoZakupuProduktu = dto.PriceNettAfterDiscountForProductLogisticUnit,
        KosztDostawy = dto.shipping_cost
    };

});
app.Run();