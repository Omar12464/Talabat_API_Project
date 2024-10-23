using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;

namespace Talabat_Repository.Data
{
    public class StoreContextSeed
    {
        #region Brand
        public async static Task SeedAsync(StoreContext storeContext)
        {
            var options = new JsonSerializerOptions
            {
                MaxDepth=64,
                //ReferenceHandler=ReferenceHandler.Preserve,
                //PropertyNameCaseInsensitive=true
                
            };
            var brandsData = File.ReadAllText("../Talabat_Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData,options);
            if (brands.Count() > 0)
            {
                if (storeContext.Brands.Count() == 0)
                {
                    if (brands?.Count() > 0)
                    {
                        foreach (var brand in brands)
                        {
                            storeContext.Set<ProductBrand>().Add(new ProductBrand { Name = brand.Name, Productss = brand.Productss });
                        }
                        await storeContext.SaveChangesAsync();
                    }
                }
            }

            var categorydata = File.ReadAllText("../Talabat_Repository/Data/DataSeeding/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductType>>(categorydata,options);
            if (categories.Count > 0)
            {
                if (storeContext.Caetgory.Count() == 0)
                {
                    if (categories?.Count() > 0)
                    {
                        foreach (var category in categories)
                        {
                            storeContext.Set<ProductType>().Add(new ProductType { Name = category.Name, Products = category.Products });
                        }
                        await storeContext.SaveChangesAsync();
                    }
                }
            }

            var Productdata = File.ReadAllText("../Talabat_Repository/Data/DataSeeding/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(Productdata,options);
            if (products.Count > 0)
            {
                if (storeContext.Products.Count() == 0)
                {
                    if (products?.Count() > 0)
                    {
                        foreach (var product in products)
                        {
                            storeContext.Set<Product>().Add(product);
                        }
                        await storeContext.SaveChangesAsync();
                    }
                }
            }
            //********************************************
            //Delivery

            var deliveryData = File.ReadAllText("../Talabat_Repository/Data/DataSeeding/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
            if (products.Count > 0)
            {
                if (storeContext.DeliveryMethods.Count() == 0)
                {
                    if (deliveryMethods?.Count() > 0)
                    {
                        foreach (var deliveryMethod in deliveryMethods)
                        {
                            storeContext.Set<DeliveryMethod>().Add(deliveryMethod);
                        }
                        await storeContext.SaveChangesAsync();

                    }
                }
            }
        }
        #endregion


        
       
    }
}
