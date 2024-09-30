using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Models
{
    public class ProductBrand: ModelBase
    {
        public string Name { get; set; }
        public ICollection<Product> Productss { get; set; } = new HashSet<Product>();
    }
}
