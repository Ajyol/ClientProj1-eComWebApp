namespace eComWebApp.Server.Models
{
    public class CreateCheckoutSessionRequest
    {
        public List<ServiceItem> Services { get; set; }
    }

    public class ServiceItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
