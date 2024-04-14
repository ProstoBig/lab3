using lab3.Models;
using Lab3.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab3.Pages
{
    public class Task2Model : PageModel
    {
        private readonly ILogger<Task2Model> _logger;

        public FactoryViewModel FactoryViewModel { get; set; }

        public Task2Model(ILogger<Task2Model> logger)
        {
            _logger = logger;
            FactoryViewModel = new FactoryViewModel();
        }
        public void OnGet()
        {
            // Читання даних з файлів
            DataReader dataReader = new DataReader();
            List<Factory> factories = dataReader.ReadFactories("factory.txt");
            Dictionary<int, double> bonuses = dataReader.ReadBonuses("bonuses.txt");
			// Логіка для максимальної зарплати за цехом за стаж від 10 до 20
			Dictionary<int, double> maxSalaryByDepartment = new Dictionary<int, double>();
            foreach (var factory in factories)
            {
                if (factory.Experience >= 10 && factory.Experience <= 20)
                {
                    double totalSalary = factory.Salary + (bonuses.ContainsKey(factory.EmployeeCode) ? bonuses[factory.EmployeeCode] : 0);
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
