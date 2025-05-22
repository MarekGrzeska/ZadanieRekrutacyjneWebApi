using Dapper;
using Microsoft.Data.SqlClient;
using ZadanieRekrutacyjneWebApi.Dtos;

namespace ZadanieRekrutacyjneWebApi.ProductInfoService
{
    public class ProductInfoService
    {
        private readonly string _connectionString;

        public ProductInfoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyDbContext");
        }

        public async Task<ProductInfoDto> GetBySku(string sku)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT p.name, p.EAN, p.producer_name, p.category, p.default_image, i.qty, " +
                    "i.unit, pr.PriceNettAfterDiscountForProductLogisticUnit, i.shipping_cost from Products p " +
                    "INNER JOIN Inventories i on p.SKU = i.sku " +
                    "INNER JOIN Prices pr on p.SKU = pr.SKU " +
                    $"WHERE p.SKU = '{sku}'";
                var result = await connection.QueryAsync<ProductInfoDto>(query);
                return result.FirstOrDefault();
            }
        }
    }
}
