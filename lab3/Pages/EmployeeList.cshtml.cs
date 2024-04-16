using lab3.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab3.Pages
{
    public class EmployeeListModel : PageModel
    {
        public List<Factory> Employees { get; private set; }

        public void OnGet()
        {
            // Читання даних з файлу
            string filePath = "factory.txt"; // Передайте правильний шлях до файлу
            Employees = ReadEmployees(filePath);
        }

        private List<Factory> ReadEmployees(string filePath)
        {
            List<Factory> employees = new List<Factory>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 7)
                        {
                            Factory employee = new Factory();
                            employee.EmployeeCode = int.Parse(parts[0]);
                            employee.LastName = parts[1];
                            employee.Gender = parts[2];
                            employee.DepartmentNumber = int.Parse(parts[3]);
                            employee.Position = parts[4];
                            employee.Experience = int.Parse(parts[5]);
                            employee.Salary = double.Parse(parts[6]);

                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок при читанні файлу
                ModelState.AddModelError(string.Empty, "An error occurred while reading the file: " + ex.Message);
            }

            return employees;
        }
    }
}
