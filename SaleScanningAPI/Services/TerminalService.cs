using SaleScanningAPI.Helpers;
using SaleScanningAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaleScanningAPI.Services
{
    public class TerminalService : ITerminalService
    {
        private static List<Product> registeredProducts = new List<Product>();
        public List<Product> ScannedProducts { get; set; } = new List<Product>();
        public static bool IsPriceSet { get; private set; } = false;

        private double totalPrice;

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

            IsPriceSet = true;
        }

        public IEnumerable<Product> GetRegisteredProducts()
        {
            return registeredProducts;
        }

        public void Scan(string ProductName)
        {
            ScannedProducts.Add((Product)registeredProducts.First(p => p.ProductName == ProductName).Clone());
        }

        public void Scan(string[] ProductNames)
        {
            foreach (var productName in ProductNames)
            {
                ScannedProducts.Add((Product)registeredProducts.First(p => p.ProductName == productName).Clone());
            }
        }

        public double CalculateTotal()
        {
            var tempScannedProduct = ScannedProducts;
            var i = tempScannedProduct.Count() - 1;
            while (i >= 0)
            {
                var product = tempScannedProduct[i];
                if (DiscountChecker<Product>.IsProductDiscounted(product, tempScannedProduct))
                {
                    totalPrice += (double)product.DiscountPrice;
                    RemoveDublicates(ref tempScannedProduct, product.ProductName);
                    i = tempScannedProduct.Count() - 1;
                }
                else
                {
                    totalPrice += product.Price;
                    i--;
                }
            }
            return totalPrice;
        }


        public bool IsExist(string ProductName)
        {
            return registeredProducts.Any(p => p.ProductName == ProductName);
        }

        public void RemoveDublicates(ref List<Product> Products, string ProductName)
        {
            var productType = registeredProducts.First(p => p.ProductName == ProductName);
            for (int i = 0; i < productType.RequiredAmountForDiscount; i++)
            {
                Products.Remove(Products.First(p => p.ProductName == ProductName));
            }
        }

    }
}
