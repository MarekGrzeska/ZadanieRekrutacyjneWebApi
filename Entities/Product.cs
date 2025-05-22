namespace ZadanieRekrutacyjneWebApi.Entities
{
    // lista kolumnd do zapisu w bazie danych zgodnie z dokumentacja w pdfie
    public class Product
    {
        public int ID { get; set; }
        // customwoe ustawienie kolumny SKU jako primary key
        public string SKU { get; set; }
        public string name { get; set; }
        public decimal? EAN { get; set; }
        public string producer_name { get; set; }
        public string category { get; set; }
        public bool is_wire { get; set; }
        public bool available { get; set; }
        public bool is_vendor { get; set; }
        public string default_image { get; set; }
    }
}
