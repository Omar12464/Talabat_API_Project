using System.ComponentModel.DataAnnotations;

namespace Talabat_API.DTOs
{
    public class AddressDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string State { get; set; }
        [Required]

        public string Country { get; set; }
    }
}