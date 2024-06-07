using Microsoft.EntityFrameworkCore;
using Lamon.Data;
using Lamon.Models;

namespace Lamon.Services
{
    public class ProductService: IServiceBase<Product>
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Add(Product Model)
        {
            Model.IsActive = true;
            Model.IsDeleted = false;
            context.Products.Add(Model);
            context.SaveChanges();
            return Model.Id;
        }

        public int Delete(int id)
        {
            context.Products.Remove(context.Products.FirstOrDefault(s => s.Id == id));
            context.SaveChanges();
            return 1;
        }

        public IEnumerable<Product> Get()
        {
            return context.Products;
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();

        }

       
        public Product GetDetails(int id)
        {
            return context.Products.FirstOrDefault(s => s.Id == id);
        }

        public Product Search(string name)
        {
            return context.Products.FirstOrDefault(s => s.Name == name);
        }

        public int Update(int id, Product Model)
        {
            Product product = context.Products.FirstOrDefault(s => s.Id == id);
            product.Name = Model.Name;
            product.IsActive = Model.IsActive;
            product.IsDeleted = Model.IsDeleted;
            product.Description = Model.Description;
            product.ModifiedOn = Model.ModifiedOn;

            product.Image = Model.Image;
            product.Price = Model.Price;
            product.Quantity = Model.Quantity;
            product.ModifiedOn = Model.ModifiedOn;
            product.IsFeatured = Model.IsFeatured;

            context.Products.Update(product);
            context.SaveChanges();
            return 1;
        }
    }
}
