using System.ComponentModel.DataAnnotations;

namespace Talabat_API.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
        public string? PictureUrl { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage = "Range From 0.1 to infinite")]
        public decimal Price { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Category { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Range from 1 to infinite")]
        public int Quantity { get; set; }
    }
}