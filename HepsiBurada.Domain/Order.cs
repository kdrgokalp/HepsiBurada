namespace HepsiBurada.Domain
{
    public class Order : DomainBase
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}