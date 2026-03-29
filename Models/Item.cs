namespace BodijaApi.Models
{
    public class Item{
        public int Id { get; set; }
        public required string PartNumber { get; set; }
        public required string ProductName { get; set; }
        public float USPrice { get; set; }
        public int Quantity { get; set; }

        // public Item(string partNumber, string productName, float usPrice, int quantity = 1)
        // {
        //     PartNumber = partNumber;
        //     ProductName = productName;
        //     USPrice = usPrice;
        //     Quantity = quantity;
        // }
    }
}