using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaisseEnregistreuse.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        // Liste des produits
        public IActionResult Index()
        {
            var products = _productService.GetAll();
            return View(products);
        }
        // Détails d'un produit
        public IActionResult Details(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // Formulaire ajout
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // Ajout produit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productService.Add(product);
            return RedirectToAction("Index");
        }
        // Suppression
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }

        // Afficher le form de d'ajout et de Modification
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Form(int? id)
        {
            Product? product;
            if (id.HasValue)
            {
                product = _productService.GetById(id.Value);
                if (product == null)
                    return NotFound();
                ViewData["Title"] = "Update Product";
            }
            else
            {
                product = new Product();
                ViewData["Title"] = "Add Product";
            }
            return View(product);
        }

        // Ajout ou Modification produit
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Form(Product product)
        {
            var existingProduct = _productService.GetById(product.Id);
            if (existingProduct == null)
            {
                _productService.Add(product);
                ViewData["Title"] = "Product added successfully!";
            }
            else
            {
                _productService.Update(product);
                ViewData["Title"] = "Product updated successfully!";
            }
            return RedirectToAction("Index");
        }
    }

}
    
