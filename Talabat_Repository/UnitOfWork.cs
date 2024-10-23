using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core;
using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Repository.Data;
using Talabat_Repository.RepositoreisClasses;

namespace Talabat_Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbcontext;
        private Dictionary<string, GenericRepo<ModelBase>> _Repos;

        public UnitOfWork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _Repos = new Dictionary<string, GenericRepo<ModelBase>>();
            //ProductRepo = new GenericRepo<Product>(dbcontext);
            //BrandRepo = new GenericRepo<ProductBrand>(dbcontext);
            //CategoryRepo = new GenericRepo<ProductType>(dbcontext);
            //deliveryMethodRepo = new GenericRepo<DeliveryMethod>(dbcontext);
            //orderItemsRepo = new GenericRepo<OrderItem>(dbcontext);
            //OrderRepo = new GenericRepo<Order>(dbcontext);
        }
        //public IGenericIcs<Product> ProductRepo { get;set; }
        //public IGenericIcs<ProductBrand> BrandRepo { get;set; }
        //public IGenericIcs<ProductType> CategoryRepo { get;set; }
        //public IGenericIcs<DeliveryMethod> deliveryMethodRepo { get;set; }
        //public IGenericIcs<OrderItem> orderItemsRepo { get;set; }
        //public IGenericIcs<Order> OrderRepo { get;set; }

        public async Task<int> CompleteAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _dbcontext.DisposeAsync();
        }

        public IGenericIcs<TEntity> Repo<TEntity>() where TEntity : ModelBase
        {
            var key=typeof(TEntity).Name;
            if (!_Repos.ContainsKey(key))
            {
                var repo=new GenericRepo<TEntity>(_dbcontext)as GenericRepo<ModelBase>;
                _Repos.Add(key, repo);
            }
            return _Repos[key] as IGenericIcs<TEntity>;
        }
    }
}
