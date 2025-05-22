namespace ZadanieRekrutacyjneWebApi.CsvFileModel
{
    public class ProductCsv
    {
        public int ID { get; set; }
        public string SKU { get; set; }
        public string name { get; set; }
        public string reference_number { get; set; }
        public decimal? EAN { get; set; }
        public bool can_be_returned { get; set; }
        public string producer_name { get; set; }
        public string category { get; set; }
        public bool is_wire { get; set; }
        public string shipping { get; set; }
        public string package_size { get; set; }
        public bool available { get; set; }
        public double? logistic_height { get; set; }
        public double? logistic_width { get; set; }
        public double? logistic_length { get; set; }
        public double? logistic_weight { get; set; }
        public bool is_vendor { get; set; }
        public bool available_in_parcel_locker { get; set; }
        public string default_image { get; set; }
    }
}