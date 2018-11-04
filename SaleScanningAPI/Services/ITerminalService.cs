using SaleScanningAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleScanningAPI.Services
{
    public interface ITerminalService
    {
        void SetPricing();
        IEnumerable<Product> GetRegisteredProducts();
        IEnumerable<string> GetScannedProducts();
        void Scan(string ProductName);
        void Scan(string[] ProductNames);
        bool IsExist(string ProductName);
    }
}
