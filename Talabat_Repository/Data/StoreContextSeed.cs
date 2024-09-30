using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat_Core.Models;

namespace Talabat_Repository.Data
{
    public class StoreContextSeed
    {
        #region Brand
        public async static Task SeedAsync(StoreContext storeContext)
        {
            var brandsData = File.ReadAllText("../Talabat_Repository/Data/DataSeeding/Brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands.Count() > 0)
            {
                if (storeContext.Brands.Count() == 0)
                    foreach (var brand in brands)
                    {
                        storeContext.Set<ProductBrand>().Add(brand);
                    }
                storeContext.SaveChanges();
            }

            var categorydata = File.ReadAllText("../Talabat_Repository/Data/DataSeeding/Categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductType>>(categorydata);
            if (categories.Count > 0)
            {
                if (storeContext.Caetgory.Count() == 0)
                {
                    foreach (var category in categories)
                    {
                        storeContext.Set<ProductType>().Add(category);
                    }
                }
                storeContext.SaveChanges();
            }

            var Productdata = File.ReadAllText("../Talabat_Repository/Data/DataSeeding/Products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(Productdata);
            if (products.Count > 0)
            {
                if (storeContext.Products.Count() == 0)
                {
                    foreach (var product in products)
                    {
                        storeContext.Set<Product>().Add(product);
                    }
                }
                storeContext.SaveChanges();
            }
        }
        #endregion


        
       
    }
}
