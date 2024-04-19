using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab3.Services;
using lab3.Models;
using System.Collections.Generic;

namespace lab3.Pages
{
    public class BonusesModel : PageModel
    {
        private readonly DataReader _dataReader;

        public List<Bonus> Bonuses { get; set; }

        public BonusesModel(DataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public void OnGet()
        {
            Bonuses = _dataReader.GetBonuses();
        }
    }
}
