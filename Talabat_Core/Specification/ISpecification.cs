using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Specification
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; }

        public Expression<Func<T,object>> OrderByASC { get; set; }
        public Expression<Func<T, object>> OrderByDESC { get; set; }

    }
}
