using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dashboard.Models.Interfaces
{
    [XmlInclude(typeof(Report))]
    public interface IReport : IDashboradItem
    {
        string ReportInformation { get; set; }
        string FilePath { get; }
        DateTime UploadDateTime { get; set; }
        IEnumerable<IPeriod> Periods { get; set; }
    }
}
