using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string SkuCode { get; set; }
        public string Name { get; set; }

    }
}