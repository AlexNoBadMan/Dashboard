using Dashboard.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class Report //: IReport
    {
        public string Name { get; set; }
        public string ReportInformation { get; set; }
        public string FilePath { get; }
        public DateTime UploadDateTime { get; set; }
        public List<Period> Periods { get; set; }

        public Report()
        {

        }

        public Report(string organizationName, string reportInformation, string filePath, List<Period> periods)
        {
            Name = organizationName;
            ReportInformation = reportInformation;
            UploadDateTime = GetUploadDateTime();
            FilePath = filePath;
            Periods = periods;
        }

        private DateTime GetUploadDateTime()
        {
            var stringDate = ReportInformation.Replace("Отчет составлен ", ""); 
            if (!DateTime.TryParse(stringDate, out var dateTime))
            {
                if (File.Exists(FilePath))
                    dateTime = File.GetCreationTime(FilePath); 
            }
            return dateTime;
        }
        public override string ToString()
        {
            return ReportInformation;
        }
    }
}
