using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Models
{
    public class ProductType: ModelBase
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
