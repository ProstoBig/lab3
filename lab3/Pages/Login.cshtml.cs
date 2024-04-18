using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab3.Models;
using Lab3.Services;

namespace lab3.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DataReader _dataReader;

        [BindProperty]
        public User User { get; set; }

        public LoginModel(DataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var users = _dataReader.ReadUsers("users.txt");

            foreach (var user in users)
            {
                if (user.Username == User.Username && user.Password == User.Password)
                {
                    HttpContext.Session.SetString("Username", User.Username);
                    return RedirectToPage("/Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Невірний логін або пароль");
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
