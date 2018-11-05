using SaleScanningAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleScanningAPI.Services
{
    public interface ITerminalService
    {
        List<Product> ScannedProducts { get; set; }

        void SetPricing();
        IEnumerable<Product> GetRegisteredProducts();
        void RemoveDublicates(ref List<Product> Products, string ProductName);
        void Scan(string ProductName);
        void Scan(string[] ProductNames);
        bool IsExist(string ProductName);
        double CalculateTotal();
    }
}
