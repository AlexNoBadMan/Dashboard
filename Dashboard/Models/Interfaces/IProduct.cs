using System.Xml.Serialization;

namespace Dashboard.Models.Interfaces
{
    [XmlInclude(typeof(Product))]
    public interface IProduct : IDashboradItem
    {
        IRevenue Revenue { get; set; }
    }
}
