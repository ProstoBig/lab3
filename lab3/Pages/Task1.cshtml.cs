// Файл Task1.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab3.Services;
using lab3.Models;

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
            Dictionary<int, double> bonuses = dataReader.ReadBonuses("bonuses.txt");

            // Виконання завдань
            int femaleEmployeesWithBonusCount = 0;
            foreach (var factory in factories)
            {
                if (factory.Gender == "Female" && bonuses.ContainsKey(factory.EmployeeCode))
                {
                    femaleEmployeesWithBonusCount++;
                }
            }
            FactoryViewModel.FemaleEmployeesWithBonusCount = femaleEmployeesWithBonusCount;
        }
    }
}
