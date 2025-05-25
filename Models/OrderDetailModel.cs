using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class OrderDetailModel
    {
        public Nullable<int> IdOrd { get; set; }
        public Nullable<int> IDPro { get; set; }
        public string NamePro { get; set; }
        public string NameCusOrd { get; set; }
        public string AddressOrd { get; set; }
        public Nullable<int> QuanityOrd { get; set; }
        public Nullable<double> PriceOrd { get; set;}
        public Nullable<System.DateTime> DateOrd { get; set; }
    }
}