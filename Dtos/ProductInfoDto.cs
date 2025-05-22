namespace ZadanieRekrutacyjneWebApi.Dtos
{
    // model dto zwracany przez metode get, zgodny z wymaganiami projektu
    public class ProductInfoDto
    {
        public string Name { get; set; }
        public decimal? EAN { get; set; }
        public string Producer_name { get; set; }
        public string Category { get; set; }
        public string Default_image { get; set; }
        public double qty { get; set; }
        public string unit { get; set; }
        public double PriceNettAfterDiscountForProductLogisticUnit { get; set; }
        public double shipping_cost { get; set; }
    }
}

//Mapowanie zwracanych danych
//a.Nazwa produktu				        product.Name 
//b.EAN						            product.EAN
//c.Nazwa producenta				    product.Producer_name
//d.Kategoria					        product.Category
//e.URL do zdjęcia produktu			    product.Default_image
//f.Stan magazynowy				        inventory.qty
//g.Jednostkę logistyczną produktu		inventory.unit
//h.Cenę netto zakupu produktu			price.PriceNettAfterDiscountForProductLogisticUnit
//i.Koszt dostawy					    inventory.shipping_cost