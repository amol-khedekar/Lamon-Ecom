using System.ComponentModel.DataAnnotations;

namespace Lamon.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string NameonCard { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required,CreditCard]
        public string CreditcardNumber { get; set; }

        public string? StatusOfOrder { get; set; }

        public string? Quantities { get; set; }

        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }

    }
}
