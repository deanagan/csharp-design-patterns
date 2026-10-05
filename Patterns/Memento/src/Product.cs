namespace Memento
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public Product ShallowCopy()
        {
            return (Product)this.MemberwiseClone();
        }

    }
}
