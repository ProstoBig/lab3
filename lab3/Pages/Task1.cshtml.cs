using Microsoft.AspNetCore.Mvc.RazorPages;
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
            DataReader dataReader = new DataReader();
            List<Factory> factories = dataReader.ReadFactories("factory.txt");
            List<Bonus> bonuses = dataReader.ReadBonusList("bonuses.txt");

            Dictionary<string, int> femaleEmployeesWithBonusByPosition = factories
                .Where(factory => factory.Gender == "Female" && bonuses.Any(bonus => bonus.EmployeeCode == factory.EmployeeCode))
                .GroupBy(factory => factory.Position)
                .ToDictionary(group => group.Key, group => group.Count());

            FactoryViewModel.FemaleEmployeesWithBonus = femaleEmployeesWithBonusByPosition;
        }
    }
}
