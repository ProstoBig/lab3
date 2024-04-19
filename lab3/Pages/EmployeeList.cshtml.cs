using Lab3.Services;
using lab3.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace lab3.Pages
{
    public class EmployeeListModel : PageModel
    {
        private readonly DataReader _dataReader;

        public List<Factory> Employees { get; private set; }

        public EmployeeListModel(DataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public void OnGet()
        {
            Employees = _dataReader.GetFactories();
        }
    }
}