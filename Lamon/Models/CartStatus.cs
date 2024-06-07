namespace Lamon.Models
{
    public class CartStatus
    {
        public int Id { get; set; }

        [NameExist]
        public string? cartStatus { get; set; }
    }
}
