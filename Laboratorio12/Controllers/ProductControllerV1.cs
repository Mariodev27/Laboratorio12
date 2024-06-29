using Laboratorio12.Models;
using Laboratorio12.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductControllerV1 : ControllerBase
    {
        private readonly DemoContext demoContext;

        public ProductControllerV1(DemoContext demoContext)
        {
            this.demoContext = demoContext;
        }

        [HttpPost]
        public Product InsertProducts(ProductV1 productData)
        {
            var product = new Product
            {
                Name = productData.Name,
                Price = (float)productData.Price, // Asegúrate de que el modelo acepte float
                Active = 1,
            };

            demoContext.Products.Add(product);
            demoContext.SaveChanges();

            return product;
        }

        [HttpDelete("{id}")]
        public Product DeleteProduct(int id)
        {
            Product product = demoContext.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                product.Active = 0;
                demoContext.Entry(product).State = EntityState.Modified;
                demoContext.SaveChanges();
            }

            return product;
        }

        [HttpPut]
        public Product UpdateProduct(ProductV2 productData)
        {
            Product product = demoContext.Products.FirstOrDefault(x => x.ProductId == productData.Id);
            if (product != null)
            {
                product.Price = (float)productData.Precio; // Asegúrate de que el modelo acepte float
                demoContext.Entry(product).State = EntityState.Modified;
                demoContext.SaveChanges();
            }

            return product;
        }

        [HttpDelete]
        public List<Product> DeleteListProducts([FromBody] List<int> productIds)
        {
            List<Product> updatedProducts = new List<Product>();

            foreach (var productId in productIds)
            {
                var product = demoContext.Products.Find(productId);
                if (product != null)
                {
                    product.Active = 0;
                    demoContext.Entry(product).State = EntityState.Modified;
                    updatedProducts.Add(product);
                }
            }

            demoContext.SaveChanges();
            return updatedProducts;
        }
    }
}