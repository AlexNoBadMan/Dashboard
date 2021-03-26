using System.Xml.Serialization;

namespace Dashboard.Models.Interfaces
{
    [XmlInclude(typeof(Revenue))]
    public interface IRevenue
    {
        double Plan { get; set; }
        double Fact { get; set; }
        double PercentageCompletion { get; }
    }
}