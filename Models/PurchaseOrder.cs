namespace BodijaApi.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public required string PurchaseOrderNumber { get; set; }
        public string ? PhoneNo { get; set; }
        public string ? Email { get; set; }
        public string ? DeliveryNotes { get; set; }
        public DateOnly OrderDate { get; set; }
        public Address ? ShippingAddress { get; set; }
        public Address ? BillingAddress { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}   

















