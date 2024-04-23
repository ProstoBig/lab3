using Lab3.Services;
using lab3.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
namespace lab3.Pages
{
    public class EmployeeListModel : PageModel
    {
        private readonly IMemoryCache _cache;

        public List<Factory> Employees { get; private set; }

        public EmployeeListModel(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void OnGet()
        {
            Employees = _cache.Get<List<Factory>>("Employees");

            if (Employees == null)
            {
                var dataReader = new DataReader(_cache);
                Employees = dataReader.GetFactories();

                _cache.Set("Employees", Employees, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                });
            }
        }
    }
}
