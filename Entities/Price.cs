namespace ZadanieRekrutacyjneWebApi.Entities
{
    // do bazy danych zapisuje tylko dane wymagane do spełnienia wymagań drugiego endpointu
    //h.Cenę netto zakupu produktu			price.PriceNettAfterDiscountForProductLogisticUnit
    public class Price
    {
        // dodana kolumna id jako primary key 
        public int Id { get; set; }
        public string SKU { get; set; }
        public double PriceNettAfterDiscountForProductLogisticUnit { get; set; }
    }
}
