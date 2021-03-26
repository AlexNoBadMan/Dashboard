using Dashboard.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class Product //: IProduct
    {
        public string Name { get; set; }
        public Revenue Revenue { get; set; }

        public Product(string name, Revenue revenue)
        {
            Name = name;
            Revenue = revenue;
        }

        public Product()
        {

        }
    }
}
