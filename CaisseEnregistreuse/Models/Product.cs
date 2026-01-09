using System.ComponentModel.DataAnnotations.Schema;

namespace CaisseEnregistreuse.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable => Stock > 0;
        public List <CartItem> CartItems { get; set; }

    }

}
