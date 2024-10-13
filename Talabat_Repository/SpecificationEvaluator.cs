using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;
using Talabat_Core.Specification;

namespace Talabat_Repository
{
    public class SpecificationEvaluator<TEntity> where TEntity : ModelBase
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> innerquery,ISpecification<TEntity> spec)
        {
            var query = innerquery;
            if(spec.Criteria is not null)
            {
                query=query.Where(spec.Criteria);
            }
            if (spec.OrderByASC != null)
            {
                query = query.OrderBy(spec.OrderByASC);
            }
            else if (spec.OrderByDESC != null)
            {
                query=query.OrderBy(spec.OrderByDESC);
            }
            if (spec.Includes != null && spec.Includes.Any())
            {
                query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            }

            return query;

             
        }
    }
}
