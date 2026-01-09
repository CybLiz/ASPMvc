using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Models;
using Microsoft.EntityFrameworkCore;


namespace CaisseEnregistreuse.Service
{
        public class CategoryService : IService<Category>
        {
        /*         public static List<Category> categories = new List<Category>
         {
             new Category
             {
                 Id = 1,
                 Name = "Electronics",
                 Products = new List<Product>
                 {
                     new Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10 },
                     new Product { Id = 2, Name = "Smartphone", Price = 499.99m, Stock = 0 }
                 }
             },
             new Category { Id = 2, Name = "Books", Products = new List<Product>() },
             new Category
             {
                 Id = 3,
                 Name = "Clothing",
                 Products = new List<Product>
                 {
                     new Product { Id = 4, Name = "Coat", Price = 199.99m, Stock = 15 }
                 }
             }
         };
         private static int _nextId = 4;*/



        // Récupérer toutes les catégories
        /* public List<Category> GetAll()
         {
             return categories;
         }
 */

        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        // Récupérer une catégorie par Id
        public Category? GetById(int id)
        {

            return _context.Categories
                           .Include(c => c.Products)
                           .FirstOrDefault(c => c.Id == id);
        }
        // Ajouter une nouvelle catégorie
        public void Add(Category entity)
        {

            _context.Categories.Add(entity);
            _context.SaveChanges();
        }
        // Supprimer une catégorie par Id
        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        // Mettre à jour une catégorie
        public void Update(Category entity)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == entity.Id);
            if (category != null)
            {
                category.Name = entity.Name;
                _context.SaveChanges();
            }

        }

        }
}
