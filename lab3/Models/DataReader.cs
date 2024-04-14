using System;
using System.Collections.Generic;
using System.IO;
using lab3.Models;

namespace Lab3.Services
{
    public class DataReader
    {
        public List<Factory> ReadFactories(string filePath)
        {
            List<Factory> factories = new List<Factory>();

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
                            Factory factory = new Factory();
                            factory.EmployeeCode = int.Parse(parts[0]);
                            factory.LastName = parts[1];
                            factory.Gender = parts[2];
                            factory.DepartmentNumber = int.Parse(parts[3]);
                            factory.Position = parts[4];
                            factory.Experience = int.Parse(parts[5]);
                            factory.Salary = double.Parse(parts[6]);

                            factories.Add(factory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            }

            return factories;
        }

        public Dictionary<int, double> ReadBonuses(string filePath)
        {
            Dictionary<int, double> bonuses = new Dictionary<int, double>();

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
                            int employeeCode = int.Parse(parts[0]);
                            double amount = double.Parse(parts[1]);
                            bonuses.Add(employeeCode, amount);
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
