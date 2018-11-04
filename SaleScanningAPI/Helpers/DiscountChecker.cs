using SaleScanningAPI.Models;
using SaleScanningAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleScanningAPI.Helpers
{
    public class DiscountChecker<T> where T : IProduct
    {
        public static bool Check(T ProductToCheck, List<T> ScannedProducts)
        {
            if (ProductToCheck.HasDiscount)
            {
                var sameProductsTypeCount = ScannedProducts.Where(p => p.ProductName == ProductToCheck.ProductName).Count();
                if(sameProductsTypeCount >= ProductToCheck.RequiredAmountForDiscount)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
