using Lamon.Data;
using System.ComponentModel.DataAnnotations;

namespace Lamon.Models
{
    public class NameExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                ApplicationDbContext context = new ApplicationDbContext();

                if (validationContext.ObjectType == typeof(Category))
                {
                    Category category = validationContext.ObjectInstance as Category;
                    int id = category.Id;
                    Category categoryFromDb = context.Categories.FirstOrDefault(s => s.Name == value.ToString());
                    if (categoryFromDb != null && categoryFromDb.Id != id)
                    {
                        return new ValidationResult("Category Name Already Exist");
                    }

                    return ValidationResult.Success;
                }
                else if (validationContext.ObjectType == typeof(Product))
                {
                    Product product = validationContext.ObjectInstance as Product;
                    int id = product.Id;
                    Product productFromDb = context.Products.FirstOrDefault(s => s.Name == value.ToString());
                    if (productFromDb != null && productFromDb.Id != id)
                    {
                        return new ValidationResult("Product Name Already Exist");
                    }

                    return ValidationResult.Success;
                }

            }
            return new ValidationResult("Empty");
        }
    }
}
