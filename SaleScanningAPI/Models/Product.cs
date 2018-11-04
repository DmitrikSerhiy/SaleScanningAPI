using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleScanningAPI.Models
{
    public class Product : ICloneable, IProduct
    {

        public string ProductName { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public int? RequiredAmountForDiscount { get; set; }
        public bool HasDiscount { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
