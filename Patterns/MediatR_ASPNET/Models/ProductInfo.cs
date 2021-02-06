using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class ProductInfo
    {
        [Key]
        public int Id { get; set; }
        public string SkuCode { get; set; }
    }
}