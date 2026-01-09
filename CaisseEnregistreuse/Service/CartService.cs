using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Models;
using Microsoft.EntityFrameworkCore;

namespace CaisseEnregistreuse.Service
{
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public Cart GetCartByUser(int userId)
        {
            var cart = _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            return cart;
        }

        private void cartTotal(Cart cart)
        {
            cart.Total = cart.Items.Sum(i => i.Quantity * i.Product.Price);
        }

        /*   public void AddProduct(int userId, int productId)
           {
               var cart = GetCartByUser(userId);

               if (cart == null)
               {
                   cart = new Cart { UserId = userId };
                   _context.Carts.Add(cart);
                   _context.SaveChanges();
               }

               var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
               if (item == null)
               {
                   cart.Items.Add(new CartItem
                   {
                       ProductId = productId,
                       Quantity = 1,
                   });
               }
               else
               {
                   item.Quantity++;
               }
               cartTotal(cart);
               _context.SaveChanges();
           }*/

        public void AddProduct(int userId, int productId)
        {
            var cart = GetCartByUser(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return;


            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            cartTotal(cart);
            _context.SaveChanges();
        }


        public void ClearCart(int userId)
        {
            var cart = GetCartByUser(userId);
            if (cart == null) return;

            _context.CartItems.RemoveRange(cart.Items);
            _context.SaveChanges();
        }
    }
}
