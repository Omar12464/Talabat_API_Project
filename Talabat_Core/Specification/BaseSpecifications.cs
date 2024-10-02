using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;

namespace Talabat_Core.Specification
{
    public class BaseSpecifications<T> : ISpecification<T> where T : ModelBase
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecifications()
        {
            
        }
        public BaseSpecifications(Expression<Func<T,bool>> criteriaexpression)
        {
            Criteria = criteriaexpression;
        }
    }
}
