using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Specification;
using Talabat_Core.Specification.ProductSpecifications;

namespace Talabat_Core.Models
{
    public class ProductWithBrand_Category:BaseSpecifications<Product>
    {
        public ProductWithBrand_Category(ProductSpecification specParams) :base(p=>
            (!specParams.brandId.HasValue||p.BrandId == specParams.brandId.Value)&&
            (!specParams.catId.HasValue || p.CategoryId == specParams.catId.Value)
            )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
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
            else
            {
                AddOrderByASC(p => p.Id);

            }
            ApplyPagination((specParams.pageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }
        public ProductWithBrand_Category(int id):base(p=>p.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
