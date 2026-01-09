using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Models;
using System.Collections.Generic;
using System.Linq;
namespace CaisseEnregistreuse.Service
{
    public class ProductService : IService<Product>
    {
        /*  private static List<Product> products = new List<Product>
        {
             new Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10 },
             new Product { Id = 2, Name = "Smartphone", Price = 499.99m, Stock = 0 },
             new Product { Id = 3, Name = "Tablet", Price = 299.99m, Stock = 5 },
             new Product { Id = 4, Name = "Coat", Price = 199.99m, Stock = 15 },
         };
         private static int _nextId = 5;*/

        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // Récupérer tous les produits
        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        // Récupérer un produit par Id
        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
            _context.SaveChanges();
        }
        // Ajouter un nouveau produit
        public void Add(Product entity)

        {
            /*            entity.Id = _nextId++;
            */
            _context.Products.Add(entity);
            _context.SaveChanges();
        }
        // Supprimer un produit par Id
        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
        // Mettre à jour un produit
        public void Update(Product entity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == entity.Id);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Stock = entity.Stock;
                _context.SaveChanges();
            }
        }
    }
}
