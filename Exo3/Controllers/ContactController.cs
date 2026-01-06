using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Exo3.Models;

namespace Exo3.Controllers
{
    public class ContactController : Controller
    {
        private static List<Contact> contacts = new List<Contact>
        {
            new Contact { Id = 1, Nom = "Ali", Email = "ali@mail.com", Telephone = "0600000001" },
            new Contact { Id = 2, Nom = "Sara", Email = "sara@mail.com", Telephone = "0600000002" }
        };

        [HttpGet]
        public IActionResult Contacts()
        {
            ViewBag.Contacts = contacts;
            return View();
        }

        // Détail d'un contact par Id
        public IActionResult ContactById(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
                return NotFound("Contact non trouvé");

            return View(contact);
        }

        [HttpGet]
        public IActionResult Form()
        {
            ViewData["Title"] = "Ajouter un contact";
            return View();
        }

        // Creation d'un nouveau contact
        /*   [HttpPost]
           public IActionResult Form(string Email, string Password)
           {
               ViewData["Message"] = "Formulaire envoyé avec succès";
               return View();
           }*/

        [HttpPost]
        public IActionResult Form(string Email, string Password)
        {
            contacts.Add(new Contact
            {
                Id = contacts.Count + 1,
                Email = Email,
                Password = Password
            });

            return RedirectToAction("Contacts");
        }
    }
}