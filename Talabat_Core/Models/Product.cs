using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Talabat_Core.Models
{
    public class Product: ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }
        [InverseProperty("Productss")]
        [JsonIgnore]
        public ProductBrand Brand { get; set; }
        public int BrandId { get; set; }
        
        [InverseProperty("Products")]
        [JsonIgnore]

        public ProductType Category { get; set; }
        public int CategoryId { get; set; }

    }
}
