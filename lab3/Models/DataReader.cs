using System;
using System.Collections.Generic;
using System.IO;
using lab3.Models;

namespace Lab3.Services
{
    public class DataReader
    {
        private List<Factory> _factories;
        private List<Bonus> _bonuses;
        private List<User> _users;

        public DataReader()
        {
            _factories = ReadFactories("factory.txt");
            _bonuses = ReadBonusList("bonuses.txt");
            _users = ReadUsers("users.txt");
        }

        public List<Factory> GetFactories()
        {
            return _factories;
        }

        public List<Bonus> GetBonuses()
        {
            return _bonuses;
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public void WriteUsers(string filePath, User user)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    sw.WriteLine($"{user.Username},{user.Password}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }

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
            catch (Exception ex)
            {
                Console.WriteLine("Введено не вірні кількість даних");
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

                            Bonus bonus = new Bonus();
                            bonus.EmployeeCode = int.Parse(parts[0]);
                            bonus.Amount = double.Parse(parts[1]);
                            bonuses.Add(bonus);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Введено не вірні кількість даних");
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

                            User user = new User();
                            user.Username = parts[0];
                            user.Password = parts[1];
                            users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Введено не вірні кількість даних");
            }

            return users;
        }
    }
}
