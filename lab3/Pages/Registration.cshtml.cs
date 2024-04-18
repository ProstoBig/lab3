using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab3.Models;
using Lab3.Services;
using System;

namespace lab3.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DataReader dataReader = new DataReader();
                User existingUser = dataReader.ReadUsers("users.txt").Find(u => u.Username == User.Username);

                if (existingUser == null)
                {
                    dataReader.WriteUsers("users.txt", new List<User> { User });
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Користувач з таким ім'ям користувача вже існує.");
                }
            }

            return Page();
        }
    }
}