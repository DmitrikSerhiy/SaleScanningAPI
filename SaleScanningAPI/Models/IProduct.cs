using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleScanningAPI.Models
{
    public interface IProduct
    {
        string ProductName { get; set; }
        double Price { get; set; }
        double? DiscountPrice { get; set; }
        int? RequiredAmountForDiscount { get; set; }
        bool HasDiscount { get; set; }
    }
}
