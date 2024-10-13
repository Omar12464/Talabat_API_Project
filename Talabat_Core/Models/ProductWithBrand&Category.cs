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
        public ProductWithBrand_Category(string sort):base()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderByASC(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDESC(P => P.Price);
                        break;
                    case "brandAsc":
                        AddOrderByASC(p => p.BrandId);
                        break;
                    case "brandDesc":
                        AddOrderByDESC(p => p.BrandId);
                        break;
                    default:
                        AddOrderByASC(p => p.Name);
                        break;

                }
            }
        }
        public ProductWithBrand_Category(int id):base(p=>p.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
