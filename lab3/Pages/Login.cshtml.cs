using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using lab3.Models;

namespace lab3.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            // Логіка для перевірки введених даних та авторизації
            if (User.Username == "1" && User.Password == "1")
            {
                HttpContext.Session.SetString("Username", User.Username);
                return RedirectToPage("/Index"); // Перенаправлення на головну сторінку
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Невірний логін або пароль");
                return Page();
            }
        }

        public IActionResult OnPostLogout()
        {
            System.Diagnostics.Debug.WriteLine("Logout method called");

            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }
    }
}
