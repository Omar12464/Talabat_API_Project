using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Specification;

namespace Talabat_Core.Models
{
    public class ProductWithBrand_Category:BaseSpecifications<Product>
    {
        public ProductWithBrand_Category():base()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

        }
        public ProductWithBrand_Category(int id):base(p=>p.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
