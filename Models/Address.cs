namespace BodijaApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public required string Name { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zip { get; set; }
        public required string Country { get; set; }

        // public Address(string type, string name, string street, string city, string state, string zip, string country)
        // {
        //     Type = type;
        //     Name = name;
        //     Street = street;
        //     City = city;
        //     State = state;
        //     Zip = zip;
        //     Country = country;
        // }
    }
}