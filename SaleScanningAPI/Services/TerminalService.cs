using SaleScanningAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleScanningAPI.Services
{
    public class TerminalService : ITerminalService
    {
        private static List<Product> registeredProducts = new List<Product>();

        private double totalPrice;
        private List<string> basket;
        public TerminalService()
        {
            basket = new List<string>();
        }

        public void SetPricing()
        {
            registeredProducts.AddRange(
                new List<Product> {
                    new Product
                    {
                        ProductName = "A",
                        Price = 1.25,
                        HasDiscount = true,
                        DiscountPrice = 3,
                        RequiredAmountForDiscount = 3
                    },
                    new Product
                    {
                        ProductName = "B",
                        Price = 4.25,
                        HasDiscount = false,
                    },
                    new Product
                    {
                        ProductName = "C",
                        Price = 1.0,
                        HasDiscount = true,
                        DiscountPrice = 5.0,
                        RequiredAmountForDiscount = 6
                    },
                    new Product
                    {
                        ProductName = "D",
                        Price = 0.75,
                        HasDiscount = false
                    }
                });
        }

        public IEnumerable<Product> GetRegisteredProducts()
        {
            return registeredProducts;
        }

        public void Scan(string ProductName)
        {
            basket.Add(ProductName);
        }

        public void Scan(string[] ProductNames)
        {
            basket.AddRange(ProductNames);
        }

        public double CalculateTotal()
        {
            
            return totalPrice;
        }


        public bool IsExist(string ProductName)
        {
            return registeredProducts.Any(p => p.ProductName == ProductName);
        }

        private Product GetProduct(string ProductName)
        {
            return registeredProducts.First(p => p.ProductName == ProductName);
        }

        public IEnumerable<string> GetScannedProducts()
        {
            return basket.ToList();
        }
    }
}
