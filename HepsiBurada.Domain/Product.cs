namespace HepsiBurada.Domain
{
    public class Product : DomainBase
    {
        public string ProductCode { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}