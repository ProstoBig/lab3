using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using lab3.Models;
using Lab3.Services;

namespace Lab3.Pages
{
    public class Task1Model : PageModel
    {
        private readonly ILogger<Task1Model> _logger;
        private readonly IMemoryCache _cache;

        public FactoryViewModel FactoryViewModel { get; set; }

        public Task1Model(ILogger<Task1Model> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
            FactoryViewModel = new FactoryViewModel();
        }

        public void OnGet()
        {
            List<Factory> factories = _cache.Get<List<Factory>>("Factories");
            List<Bonus> bonuses = _cache.Get<List<Bonus>>("Bonuses");

            if (factories == null || bonuses == null)
            {
                var dataReader = new DataReader(_cache);
                factories = dataReader.GetFactories();
                bonuses = dataReader.GetBonuses();

                _cache.Set("Factories", factories, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                });

                _cache.Set("Bonuses", bonuses, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                });
            }

            Dictionary<string, int> femaleEmployeesWithBonusByPosition = new Dictionary<string, int>();

            foreach (var factory in factories)
            {
                if (factory.Gender == "Female" && HasBonus(bonuses, factory.EmployeeCode))
                {
                    if (femaleEmployeesWithBonusByPosition.ContainsKey(factory.Position))
                    {
                        femaleEmployeesWithBonusByPosition[factory.Position]++;
                    }
                    else
                    {
                        femaleEmployeesWithBonusByPosition[factory.Position] = 1;
                    }
                }
            }

            FactoryViewModel.FemaleEmployeesWithBonus = femaleEmployeesWithBonusByPosition;
        }

        private bool HasBonus(List<Bonus> bonuses, int employeeCode)
        {
            foreach (var bonus in bonuses)
            {
                if (bonus.EmployeeCode == employeeCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
