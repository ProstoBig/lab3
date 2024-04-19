using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab3.Models;
using Lab3.Services;
using System;
using System.Collections.Generic;

namespace lab3.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly DataReader _dataReader;

        [BindProperty]
        public User User { get; set; }

        public RegistrationModel(DataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                List<User> existingUsers = _dataReader.ReadUsers("users.txt");

                User existingUser = null;
                foreach (var user in existingUsers)
                {
                    if (user.Username == User.Username)
                    {
                        existingUser = user;
                        break;
                    }
                }

                if (existingUser == null)
                {
                    _dataReader.WriteUsers("users.txt", User);
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
