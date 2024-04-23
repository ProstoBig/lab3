using System;
using System.Collections.Generic;
using System.IO;
using lab3.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Lab3.Services
{
    public class DataReader
    {
        private readonly IMemoryCache _cache;

        public DataReader(IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<Factory> GetFactories()
        {
            return GetOrCreateCache<List<Factory>>("FactoryList", ReadFactoriesFromFile);
        }

        public List<Bonus> GetBonuses()
        {
            return GetOrCreateCache<List<Bonus>>("BonusList", ReadBonusesFromFile);
        }

        private TItem GetOrCreateCache<TItem>(string key, Func<TItem> factory)
        {
            if (!_cache.TryGetValue(key, out TItem result))
            {
                result = factory();
                _cache.Set(key, result);
            }
            return result;
        }

        private List<Factory> ReadFactoriesFromFile()
        {
            List<Factory> factories = new List<Factory>();

            try
            {
                using (StreamReader sr = new StreamReader("appdata/factory.txt"))
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
                Console.WriteLine("An error occurred while reading factories from the file: " + ex.Message);
            }

            return factories;
        }

        private List<Bonus> ReadBonusesFromFile()
        {
            List<Bonus> bonuses = new List<Bonus>();

            try
            {
                using (StreamReader sr = new StreamReader("appdata/bonuses.txt"))
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
                Console.WriteLine("An error occurred while reading bonuses from the file: " + ex.Message);
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
    }
}
