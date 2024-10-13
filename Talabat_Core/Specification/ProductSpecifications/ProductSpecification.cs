using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Specification.ProductSpecifications
{
    public class ProductSpecification
    {
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? catId { get; set; }

        private int maxPage = 10;
        private int pageSize;
        public int pageIndex { get; set; } = 1;
        public int PageSize {
            get { return pageSize; }
            set { pageSize = value > maxPage ? 10 : value; }
        }




    }
}
