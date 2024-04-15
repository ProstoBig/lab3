using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using lab3.Models;
using Lab3.Services; // Додайте цей імпорт

namespace lab3.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DataReader _dataReader; // Додайте змінну _dataReader

        [BindProperty]
        public User User { get; set; }

        public LoginModel(DataReader dataReader) // Додайте конструктор, щоб внести зміни
        {
            _dataReader = dataReader;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            // Логіка для перевірки введених даних та авторизації
            // Отримайте дані з файлу користувачів
            var users = _dataReader.ReadUsers("users.txt");

            foreach (var user in users)
            {
                if (user.Username == User.Username && user.Password == User.Password)
                {
                    HttpContext.Session.SetString("Username", User.Username);
                    return RedirectToPage("/Index"); // Перенаправлення на головну сторінку
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
