using AutoMapper;
using ZadanieRekrutacyjneWebApi.CsvFileModel;
using ZadanieRekrutacyjneWebApi.Entities;

namespace ZadanieRekrutacyjneWebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductCsv>();
            CreateMap<ProductCsv, Product>();

            CreateMap<Inventory, InventoryCsv>();
            CreateMap<InventoryCsv, Inventory>();

            CreateMap<Price, PriceCsv>();
            CreateMap<PriceCsv, Price>();
        }
    }
}
