namespace ZadanieRekrutacyjneWebApi.Entities
{
    // do bazy danych zapisuje tylko dane wymagane do spełnienia wymagań drugiego endpointu
    //f.Stan magazynowy				        inventory.qty
    //g.Jednostkę logistyczną produktu		inventory.unit
    //i.Koszt dostawy					    inventory.shipping_cost
    public class Inventory
    {
        // dodana kolumna id jako primary key 
        public int Id { get; set; }
        public string sku { get; set; }
        public string unit { get; set; }
        public double qty { get; set; }
        public string shipping_cost { get; set; }
    }
}