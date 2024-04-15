using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Lab3.Services;
using lab3.Models;
using System.Linq;

namespace Lab3.Pages
{
    public class Task1Model : PageModel
    {
        private readonly ILogger<Task1Model> _logger;

        public FactoryViewModel FactoryViewModel { get; set; }

        public Task1Model(ILogger<Task1Model> logger)
        {
            _logger = logger;
            FactoryViewModel = new FactoryViewModel();
        }

        public void OnGet()
        {
            // Читання даних з файлів
            DataReader dataReader = new DataReader();
            List<Factory> factories = dataReader.ReadFactories("factory.txt");
            List<Bonus> bonuses = dataReader.ReadBonusList("bonuses.txt");

            // Виконання завдань
            int femaleEmployeesWithBonusCount = factories.Count(factory => factory.Gender == "Female" && bonuses.Any(bonus => bonus.EmployeeCode == factory.EmployeeCode));
            FactoryViewModel.FemaleEmployeesWithBonusCount = femaleEmployeesWithBonusCount;

            // Логіка для максимальної зарплати за цехом за стаж від 10 до 20
            Dictionary<int, double> maxSalaryByDepartment = new Dictionary<int, double>();
            foreach (var factory in factories)
            {
                if (factory.Experience >= 10 && factory.Experience <= 20)
                {
                    double totalSalary = factory.Salary + (bonuses.FirstOrDefault(bonus => bonus.EmployeeCode == factory.EmployeeCode)?.Amount ?? 0);
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
