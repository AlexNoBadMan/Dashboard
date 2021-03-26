using Dashboard.Models;
using Dashboard.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Dashboard.Services
{
    internal class DataService : IDataService
    {
        private static IEnumerable<string> GetDataLines(string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open);
            using var dataReader = new StreamReader(stream);
            var organization = string.Empty;
            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line) || line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).Length == 0) continue;
                yield return line;
            }
            
        }

        public Report GetData(string filePath)
        {
            var data = GetDataLines(filePath).ToList();
            var organizationName = string.Empty;
            var reportInformation = string.Empty;
            try
            {
                organizationName = data.First().Split('\t')[1];
                reportInformation = data.Last().Split('\t')[1];
            }
            catch
            {
                throw new Exception("Выбран некорректный файл");
            }
            var periods = new List<Period>();
            var header = data.FirstOrDefault(x => x.IndexOf("Персональные б", StringComparison.OrdinalIgnoreCase) > 0);
            if (header is null)
                throw new Exception("Не удалось найти Персональные бизнес-цели");

            var skipCount = data.FindIndex(x => x.IndexOf("Валовая выручка", StringComparison.OrdinalIgnoreCase) > 0);
            var splitData = data.Skip(skipCount).Select(x => x.Split('\t'));
            var columns = header.Split('\t');
            for (int i = 5; i < columns.Length - 1; i+=4)
            {
                var proceedsRow = splitData.FirstOrDefault(x=> x[2] == "Валовая выручка");
                var profitRow = splitData.FirstOrDefault(x=> x[2] == "Валовая прибыль");
                var period = new Period();
                period.Name = columns[i];
                period.GrossProceeds = new Revenue(Parse(proceedsRow[i]), Parse(proceedsRow[i + 1]));
                period.GrossProfit = new Revenue(Parse(profitRow[i]), Parse(profitRow[i + 1]));
                period.Users = GetUsersForPeriod(splitData, i).ToList();
                period.Products = GetProductsForPeriod(splitData, i).ToList();
                periods.Add(period);
            }
            return new Report(organizationName, reportInformation, filePath, periods);
        }

        private static IEnumerable<Product> GetProductsForPeriod(IEnumerable<string[]> splitData, int i)
        {
            //var products = new List<IProduct>();
            var productsData = splitData.SkipWhile(x => !x.Contains("3. Товарные показатели"));
            //foreach (var productRow in productsData)
            //{
            //    if (string.IsNullOrWhiteSpace(productRow[2]))
            //        continue;

            //    products.Add(new Product(productRow[2], new Revenue(Parse(productRow[i]), Parse(productRow[i + 1]))));
            //}
            return productsData.Where(x => !string.IsNullOrWhiteSpace(x[2])).Select(x => new Product(x[2], new Revenue(Parse(x[i]), Parse(x[i + 1]))));
        }

        private static List<User> GetUsersForPeriod(IEnumerable<string[]> splitData, int i)
        {
            var users = new List<User>();
            foreach (var proceedsRow in splitData)
            {
                if (proceedsRow[2].Contains("Валовая прибыль")) 
                    break;
                if (string.IsNullOrWhiteSpace(proceedsRow[3]) || proceedsRow[2].Contains("Валовая выручка")) 
                    continue;

                var name = proceedsRow[3];
                var profitRow = splitData.LastOrDefault(x => x[3] == name);
                var user = new User(name,
                    new Revenue(Parse(proceedsRow[i]), Parse(proceedsRow[i + 1])),
                    new Revenue(Parse(profitRow[i]), Parse(profitRow[i + 1])));
                users.Add(user);
            }
            return users;
        }

        private static double Parse(string value)
        {
            double.TryParse(value, out var dvlaue);
            return dvlaue;
        }
    }
}