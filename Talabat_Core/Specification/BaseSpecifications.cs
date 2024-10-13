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
        public Expression<Func<T, object>> OrderByASC { get ; set; }
        public Expression<Func<T, object>> OrderByDESC { get; set; }
        public int Take { get ; set ; }
        public int Skip { get ; set  ; }
        public bool IsPaginationEnable { get  ; set  ; }=false;

        public BaseSpecifications()
        {
            
        }
        public BaseSpecifications(Expression<Func<T,bool>> criteriaexpression)
        {
            Criteria = criteriaexpression;
        }

        public void AddOrderByASC(Expression<Func<T,object>> orderBy)
        {
            OrderByASC = orderBy;
        }
        public void AddOrderByDESC(Expression<Func<T, object>> orderBy)
        {
            OrderByDESC = orderBy;
        }
        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnable = true;
            Skip = skip;  
            Take = take;
        }


    }
}
