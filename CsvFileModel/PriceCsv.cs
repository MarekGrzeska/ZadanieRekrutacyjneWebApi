namespace ZadanieRekrutacyjneWebApi.CsvFileModel
{
    public class PriceCsv
    {
        public string InternalId { get; set; }
        public string SKU { get; set; }
        public double PriceNett { get; set; }
        public double PriceNettAfterDiscount { get; set; }
        public double VATRate { get; set; }
        public double PriceNettAfterDiscountForProductLogisticUnit { get; set; } 
    }
}
