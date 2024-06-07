using System.ComponentModel.DataAnnotations;

namespace Lamon.Models
{
    public class Category
    {
        public int Id { get; set; }

        [NameExist]
        [Required(ErrorMessage ="Category Name Required")]
        [StringLength(100,ErrorMessage = "Miniumum 3 and maximum 100 characters are allowed",MinimumLength =3)] 
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
