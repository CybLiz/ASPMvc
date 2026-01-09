using Microsoft.AspNetCore.Mvc;
using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Service;

namespace CaisseEnregistreuse.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService =  categoryService;
        }

        // Liste des catégories
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        // Détails d'une catégorie + produits
        public IActionResult Details(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // Formulaire ajout
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Ajout catégorie
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }

        // Suppression
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index");
        }

        // Afficher le form de Modification
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // Mise à jour
        [HttpPost]
        public IActionResult Update(Category category)
        {
            _categoryService.Update(category);
            return RedirectToAction("Index");
        }


        //Display Generic form create/update
        [HttpGet]
        public IActionResult Form(int? id)
        {
            Category category;

            if (id.HasValue) 
            {
                category = _categoryService.GetById(id.Value);
                if (category == null) return NotFound();
                ViewData["Title"] = "Update Category";
            }
            else 
            {
                category = new Category();
                ViewData["Title"] = "Add a Category";
            }

            return View(category);
        }

        // Create or update
        [HttpPost]
        public IActionResult Form(Category category)
        {
            var existingCategory = _categoryService.GetById(category.Id);
            if (existingCategory == null)
            {
                _categoryService.Add(category);
                ViewData["Message"] = "Category added successfully!";
            
            }
            else 
            {
                _categoryService.Update(category);
                ViewData["Message"] = "Category updated successfully!";
            }

            return View(category);
        }


    }
}
