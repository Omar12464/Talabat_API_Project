using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Repositories_InterFaces
{
    public interface IGenericIcs<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
