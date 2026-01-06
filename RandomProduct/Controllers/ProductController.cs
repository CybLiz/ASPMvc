using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RandomProduct.Models;

namespace RandomProduct.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Models.Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10 },
            new Models.Product { Id = 2, Name = "Smartphone", Price = 499.99m, Stock = 0 },
            new Models.Product { Id = 3, Name = "Tablet", Price = 299.99m, Stock = 5 }
        };

        [HttpGet]
        public IActionResult Products()
        {
            ViewBag.Products = products;
            return View();
        }

        // Détail d'un produit par Id
        public IActionResult ProductDetails(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found");
            return View(product);
        }

        // Creation d'un nouveau produit aléatoire
        [HttpPost]
        public IActionResult CreateRandomProduct()
        {
            var random = new Random();
            var newProduct = new Product
            {
                Id = products.Count + 1,
                Name = Product.RandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 10),
                Price = Math.Round((decimal)(random.NextDouble() * 1000), 2),
                Stock = random.Next(0, 50)
            };
            products.Add(newProduct);
            return RedirectToAction("Products");

        }

        // Suppression d'un produit par Id
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound("Product not found");
            products.Remove(product);
            return RedirectToAction("Products");
        }
    }
}
