﻿using lab3.Models;

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

        public List<Bonus> ReadBonusList(string filePath)
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
        public List<User> ReadUsers(string filePath)
        {
            List<User> users = new List<User>();

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
                            User user = new User();
                            user.Username = parts[0];
                            user.Password = parts[1];
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            }

            return users;
        }
        public void WriteUsers(string filePath, List<User> users)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    foreach (var user in users)
                    {
                        sw.WriteLine($"{user.Username},{user.Password}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }

    }
}
