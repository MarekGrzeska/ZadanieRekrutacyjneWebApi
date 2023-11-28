﻿namespace ZadanieRekrutacyjneWebApi.CsvFileModel
{
    public class InventoryCsv
    {
        public int product_id { get; set; }
        public string sku { get; set; }
        public string unit { get; set; }
        public double qty { get; set; }
        public string manufacturer_name { get; set; }
        public string manufacturer_ref_num { get; set; }
        public string shipping { get; set; }
        public string shipping_cost { get; set; }
    }
}
