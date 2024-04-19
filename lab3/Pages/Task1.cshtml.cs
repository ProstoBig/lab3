using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Lab3.Services;
using lab3.Models;
using System.Collections.Generic;

namespace Lab3.Pages
{
    public class Task1Model : PageModel
    {
        private readonly ILogger<Task1Model> _logger;
        private readonly DataReader _dataReader;

        public FactoryViewModel FactoryViewModel { get; set; }

        public Task1Model(ILogger<Task1Model> logger, DataReader dataReader)
        {
            _logger = logger;
            _dataReader = dataReader;
            FactoryViewModel = new FactoryViewModel();
            List<Factory> factories = _dataReader.GetFactories();
            List<Bonus> bonuses = _dataReader.GetBonuses();

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

        public void OnGet()
        {

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