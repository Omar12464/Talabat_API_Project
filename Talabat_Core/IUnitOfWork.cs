using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;
using Talabat_Core.Repositories_InterFaces;

namespace Talabat_Core
{
    public interface IUnitOfWork:IAsyncDisposable 
    {
        //public IGenericIcs<Product> ProductRepo { get; set; }
        //public IGenericIcs<ProductBrand> BrandRepo { get; set; }
        //public IGenericIcs<ProductType> CategoryRepo { get; set; }
        //public IGenericIcs<DeliveryMethod> deliveryMethodRepo { get; set; }
        //public IGenericIcs<OrderItem> orderItemsRepo { get; set; }
        //public IGenericIcs<Order> OrderRepo { get; set; }
        IGenericIcs<TEntity> Repo<TEntity>() where TEntity : ModelBase;
        Task<int> CompleteAsync();



    }
}
