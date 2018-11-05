using SaleScanningAPI.Models;
using SaleScanningAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SaleScanningAPI_Test
{
    public class TerminalService_Test
    {
        private ITerminalService service = new TerminalService();
        public TerminalService_Test()
        {
            if (!TerminalService.IsPriceSet)
                service.SetPricing();
        }

        [Theory]
        [InlineData("A")]
        [InlineData("D")]
        public void IsExist_Test(string ProductName)
        {
            var productExist = service.IsExist(ProductName);

            Assert.True(productExist);
        }


        [Theory]
        [InlineData("B")]
        [InlineData("C")]
        public void Scan_Test(string ProductName)
        {
            service.Scan(ProductName);

            Assert.Collection(service.ScannedProducts, 
                p => Assert.Contains(ProductName, p.ProductName));
        }

        [Fact]
        public void ScanSeveral_Test()
        {
            string[] products = new string[] { "A", "B" };

            service.Scan(products);

            Assert.Collection(service.ScannedProducts,
                p => Assert.Contains(products[0], p.ProductName ),
                p => Assert.Contains(products[1], p.ProductName));
        }


        [Theory]
        [InlineData("B")]
        [InlineData("C")]
        public void RemoveDublicates_Test(string ProductName)
        {
            //Arrange
            var product = service.GetRegisteredProducts().FirstOrDefault(p => p.ProductName == ProductName);
            var products = new List<Product>();
            for (int i = 0; i < product.RequiredAmountForDiscount; i++)
                products.Add((Product)product.Clone());

            //Act
            service.RemoveDublicates(ref products, ProductName);

            //Assert
            Assert.Empty(products);
        }

        [Theory]
        [InlineData(new string[] { "A", "B", "C", "D" }, 7.25)]
        [InlineData(new string[] { "C", "C", "C", "C", "C", "C", "C" }, 6.0)]
        [InlineData(new string[] { "A", "B", "C", "D", "A", "B", "A" }, 13.25)]
        public void CalculateTotal_Test(string[] Products, double TotalPrice)
        {
            service.Scan(Products);

            var totalPrice = service.CalculateTotal();

            Assert.Equal(TotalPrice, totalPrice);
        }
    }
}
