using Microsoft.EntityFrameworkCore;
using Lamon.Data;
using Lamon.Models;

namespace Lamon.Services
{
    public class CategoryService : IServiceBase<Category>
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Add(Category Model)
        {
            Model.IsActive = true;
            Model.IsDeleted=false;
            context.Categories.Add(Model);
            context.SaveChanges();
            return Model.Id;
        }

        public int Delete(int id)
        {
            context.Categories.Remove(context.Categories.FirstOrDefault(s => s.Id == id));
            context.SaveChanges();
            return 1;
        }

        public IEnumerable<Category> Get()
        {
            throw new NotImplementedException();
        }

        public  List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetDetails(int id)
        {
            return context.Categories.FirstOrDefault(s => s.Id == id);
        }

        public Category Search(string name)
        {
            return context.Categories.FirstOrDefault(s => s.Name == name);
        }

        public int Update(int id, Category Model)
        {
            Category category = context.Categories.FirstOrDefault(s => s.Id == id);
            category.Name = Model.Name;
            category.IsActive = Model.IsActive;
            category.IsDeleted = Model.IsDeleted;

            context.Categories.Update(category);
            context.SaveChanges();
            return 1;
        }
    }
}
