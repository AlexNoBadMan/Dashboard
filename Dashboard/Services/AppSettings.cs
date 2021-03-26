using Dashboard.Models;
using Dashboard.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dashboard.Services
{
    internal class AppSettings : IAppSettings
    {
        private readonly string _filePath;
        private readonly XmlSerializer _xmlSerializer;

        public ObservableCollection<Report> Reports { get; set; }
        public AppSettings()
        {
            var pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "Settings");
            _filePath = $"{pathFolder}\\Settings.xml";
            _xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Report>));
            if (!Directory.Exists(pathFolder))
                Directory.CreateDirectory(pathFolder);

            Reports = File.Exists(_filePath) ? GetReports() : new ObservableCollection<Report>();
        }

        private ObservableCollection<Report> GetReports()
        {
            var templates = new ObservableCollection<Report>();
            //try
            //{
            using (var stream = new FileStream(_filePath, FileMode.Open))
            {
                templates = _xmlSerializer.Deserialize(stream) as ObservableCollection<Report>;
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return templates;
        }

        public void Save()
        {
            using (var stream = new FileStream(_filePath, FileMode.Create))
            {
                _xmlSerializer.Serialize(stream, Reports);
            }
        }
    }
}
