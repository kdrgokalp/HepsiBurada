namespace HepsiBurada.Domain
{
    public class AppTime : DomainBase
    {
        public int Duration { get; set; }
        public int TurnOver { get; set; }
        public bool Active { get; set; }
    }
}