using Dashboard.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class Period //: IPeriod, IDashboradItem
    {
        public string Name { get; set; }
        public Revenue GrossProceeds { get; set; }
        public Revenue GrossProfit { get; set; }
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public double PlanSumProducts => Products.Sum(x => x.Revenue.Plan);

    }
}
