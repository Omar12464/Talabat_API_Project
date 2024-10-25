using System.ComponentModel.DataAnnotations;

namespace Talabat_API.DTOs
{
    public class AdressDtoo
    {
        [Required]
        public string FName { get; set; }
        [Required]

        public string LName { get; set; }
        [Required]

        public string Street { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Country { get; set; }
    }
}
