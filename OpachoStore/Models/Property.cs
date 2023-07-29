namespace OpachoStore.Models
{
    public class Property
    {
        public int PropertyId { get; set; }

        public Specification Specification { get; set; } = new();

        public Product Product { get; set; } = new();

        public string? Value { get; set; }
    }
}
