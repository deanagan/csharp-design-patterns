namespace Memento
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product ShallowCopy()
        {
            return (Product)this.MemberwiseClone();
        }

    }
}
