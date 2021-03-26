using Dashboard.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class User //: IUser
    {
        public string Name { get; set; }
        public Revenue GrossProceeds { get; set; }
        public Revenue GrossProfit { get; set; }
        //public double PercentageCompletion => RevenueFact / RevenuePlan * 100;
        public User(string name, Revenue grossProceeds, Revenue grossProfit)
        {
            Name = name;
            GrossProceeds = grossProceeds;
            GrossProfit = grossProfit;
        }

        public User()
        {

        }
    }
}
