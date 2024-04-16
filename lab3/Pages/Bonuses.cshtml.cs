using Microsoft.AspNetCore.Mvc.RazorPages;
using lab3.Models;

namespace lab3.Pages
{
    public class BonusesModel : PageModel
    {
        public List<Bonus> Bonuses { get; set; }

        public void OnGet()
        {
            // Завантаження даних з файлу bonuses.txt
            Bonuses = ReadBonuses("bonuses.txt");
        }

        private List<Bonus> ReadBonuses(string filePath)
        {
            List<Bonus> bonuses = new List<Bonus>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 2)
                        {
                            Bonus bonus = new Bonus();
                            bonus.EmployeeCode = int.Parse(parts[0]);
                            bonus.Amount = double.Parse(parts[1]);
                            bonuses.Add(bonus);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            }

            return bonuses;
        }
    }
}
