using Dapper;
using Microsoft.Data.SqlClient;
using ZadanieRekrutacyjneWebApi.Entities;

namespace ZadanieRekrutacyjneWebApi.InsertDataService
{
    public class InsertDataService
    {
        private readonly string _connectionString;

        public InsertDataService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyDbContext");
        }

        private async Task InsertData(string query, List<object> data)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(query, data, transaction);
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public async Task InsertProducts(List<Product> products)
        {
            var query = $"INSERT INTO Products ([SKU], [ID], [name], [EAN], [producer_name], [category], " +
                        "[is_wire], [available], [is_vendor], [default_image]) VALUES (@SKU, @ID, @name, @EAN, " +
                        "@producer_name, @category, @is_wire, @available, @is_vendor, @default_image)";

            var data = new List<object>();
            foreach (var product in products) 
            {
                data.Add(new
                {
                    SKU = product.SKU,
                    ID = product.ID,
                    name = product.name,
                    EAN = product.EAN,
                    producer_name = product.producer_name,
                    category = product.category,
                    is_wire = product.is_wire,
                    available = product.available,
                    is_vendor = product.is_vendor,
                    default_image = product.default_image,
                }) ;
            }
            await InsertData(query, data);
        }

        public async Task InsertInventories(List<Inventory> inventories)
        {
            var query = $"INSERT INTO Inventories ([SKU], [unit], [qty], [shipping_cost]) " +
                         "VALUES (@sku, @unit, @qty, @shipping_cost)";

            var data = new List<object>();
            foreach (var inventory in inventories)
            {
                data.Add(new
                {
                    SKU = inventory.sku,
                    unit = inventory.unit,
                    qty = inventory.qty,
                    shipping_cost = inventory.shipping_cost,
                });
            }
            await InsertData(query, data);
        }

        public async Task InsertPrices(List<Price> prices)
        {
            var query = $"INSERT INTO Prices ([SKU], [PriceNettAfterDiscountForProductLogisticUnit]) " +
                         "VALUES (@SKU, @PriceNettAfterDiscountForProductLogisticUnit)";

            var data = new List<object>();
            foreach (var price in prices)
            {
                data.Add(new
                {
                    SKU = price.SKU,
                    PriceNettAfterDiscountForProductLogisticUnit = price.PriceNettAfterDiscountForProductLogisticUnit,
                });
            }
            await InsertData(query, data);
        }
    }
}
