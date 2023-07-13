namespace Slots.Models.Domain
{
    public class Drink
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }        
        
        public byte[]? Avatar { get; set; }



    }
}
