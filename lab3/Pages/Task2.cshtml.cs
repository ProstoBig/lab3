using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using lab3.Models;
using Lab3.Services;

namespace lab3.Pages
{
    public class Task2Model : PageModel
    {
        private readonly ILogger<Task2Model> _logger;
        private readonly IMemoryCache _cache;

        public FactoryViewModel FactoryViewModel { get; set; }

        public Task2Model(ILogger<Task2Model> logger, IMemoryCache cache)
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

            Dictionary<int, double> maxSalaryByDepartment = new Dictionary<int, double>();

            foreach (var factory in factories)
            {
                if (factory.Experience >= 10 && factory.Experience <= 20)
                {
                    double totalSalary = factory.Salary;
                    foreach (var bonus in bonuses)
                    {
                        if (bonus.EmployeeCode == factory.EmployeeCode)
                        {
                            totalSalary += bonus.Amount;
                            break;
                        }
                    }
                    if (!maxSalaryByDepartment.ContainsKey(factory.DepartmentNumber) || totalSalary > maxSalaryByDepartment[factory.DepartmentNumber])
                    {
                        maxSalaryByDepartment[factory.DepartmentNumber] = totalSalary;
                    }
                }
            }

            FactoryViewModel.MaxSalaryByDepartment = maxSalaryByDepartment;
        }
    }
}