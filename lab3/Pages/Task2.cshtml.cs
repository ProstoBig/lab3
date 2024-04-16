using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab3.Services;
using lab3.Models;

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
            // ������� ����� � �����
            DataReader dataReader = new DataReader();
            List<Factory> factories = dataReader.ReadFactories("factory.txt");
            List<Bonus> bonuses = dataReader.ReadBonusList("bonuses.txt");

            // ����� ��� ����������� �������� �� ����� �� ���� �� 10 �� 20
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