using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Specification;

namespace Talabat_Core.Repositories_InterFaces
{
    public interface IGenericIcs<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        public Task<IReadOnlyList<T>> GettAllWithSpecAsync(ISpecification<T> spec);

        public Task<T> GettWithSpecAsync(ISpecification<T> spec);

        public Task<T> GetCountAsync(ISpecification<T> spec);
    }
}
