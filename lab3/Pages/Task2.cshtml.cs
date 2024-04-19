using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Lab3.Services;
using lab3.Models;
using System.Collections.Generic;

namespace lab3.Pages
{
    public class Task2Model : PageModel
    {
        private readonly ILogger<Task2Model> _logger;
        private readonly DataReader _dataReader;

        public FactoryViewModel FactoryViewModel { get; set; }

        public Task2Model(ILogger<Task2Model> logger, DataReader dataReader)
        {
            _logger = logger;
            _dataReader = dataReader;
            FactoryViewModel = new FactoryViewModel();

            List<Factory> factories = _dataReader.GetFactories();
            List<Bonus> bonuses = _dataReader.GetBonuses();

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

        public void OnGet()
        {

        }
    }
}
