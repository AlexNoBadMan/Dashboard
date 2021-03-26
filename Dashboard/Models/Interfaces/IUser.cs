using System.Xml.Serialization;

namespace Dashboard.Models.Interfaces
{
    [XmlInclude(typeof(User))]
    public interface IUser : IDashboradItem
    {
        IRevenue GrossProceeds { get; set; }
        IRevenue GrossProfit { get; set; }
    }
}
