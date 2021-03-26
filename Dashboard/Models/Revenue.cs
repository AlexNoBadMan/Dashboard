using Dashboard.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class Revenue : IRevenue
    {
        public double Plan { get; set; }
        public double Fact { get; set; }
        public double PercentageCompletion => Fact / Plan * 100;
        
        public Revenue(double plan, double fact)
        {
            Plan = plan;
            Fact = fact;
        }

        public Revenue()
        {

        }
    }
}
