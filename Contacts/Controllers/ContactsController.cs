using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Contacts.Models; 
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    public class ContactsController : Controller
    {
        private static string Filename = "contacts.txt";
        private static Random Rand = new();

        [HttpGet]
        public IActionResult Index()
        {
            var defContacts = new List<Contact>();
            for (int i = 0; i < Rand.Next(2, 10); i++)
            {
                defContacts.Add(new Contact
                {
                    Name = $"Name {Rand.Next(1, 100)}",
                    MobilePhone = $"Phone {Rand.Next(1000, 9999)}",
                    AlternateMobilePhone = $"Alt Phone {Rand.Next(1000, 9999)}",
                    Email = $"email{i}@example.com",
                    ShortDescription = $"Description for {i + 1} contact"
                });
            }
            return View("Index", defContacts);
        }

        [HttpPost]
        public async Task<IActionResult> SaveInFile([FromBody] List<Contact> contacts)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string jsonString = JsonSerializer.Serialize(contacts, options);
                await System.IO.File.WriteAllTextAsync(Filename, jsonString);
                return Json(new { success = true, message = "Збережено..." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Помилка..." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadFromFile()
        {
            string jsonString = await System.IO.File.ReadAllTextAsync(Filename);
            var contactsFromFile = JsonSerializer.Deserialize<List<Contact>>(jsonString);
            return PartialView("_ContactsPartial", contactsFromFile);
        }
    }
}
