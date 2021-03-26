using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dashboard.Models.Interfaces
{
    [XmlInclude(typeof(Period))]
    public interface IPeriod : IDashboradItem
    {
        IRevenue GrossProceeds { get; set; }
        IRevenue GrossProfit { get; set; }
        IEnumerable<IUser> Users { get; set; }
        IEnumerable<IProduct> Products { get; set; }
        public double PlanSumProducts { get; }
    }
}