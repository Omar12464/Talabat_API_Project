using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Core.Specification;
using Talabat_Repository.Data;

namespace Talabat_Repository.RepositoreisClasses
{
    public class GenericRepo<T> : IGenericIcs<T> where T : ModelBase
    {
        private readonly StoreContext _dbcontext;

        public GenericRepo(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return (IReadOnlyList<T>)await _dbcontext.Set<Product>()
            //        .Include(p => p.BrandId)
            //        .Include(c => c.CategoryId)
            //        .OrderBy(c=>c.Name)
            //        .ToListAsync();
            //}

            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

         async Task<IReadOnlyList<T>> IGenericIcs<T>.GettAllWithSpecAsync(ISpecification<T> spec)
        {

            return await SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec).ToListAsync();
        }


         async Task<T> IGenericIcs<T>.GettWithSpecAsync(ISpecification<T> spec)
        {
          
                return await SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec).FirstOrDefaultAsync();
          
        }
    }
}
