namespace CaisseEnregistreuse.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public decimal Total { get; set; }
    }
}
