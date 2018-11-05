using SaleScanningAPI.Helpers;
using SaleScanningAPI.Models;
using SaleScanningAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SaleScanningAPI_Test
{
    public class DiscountChecker_Test
    {
        private ITerminalService service = new TerminalService();
        public DiscountChecker_Test()
        {
            if (!TerminalService.IsPriceSet)
                service.SetPricing();
        }

        [Theory]
        [InlineData("A")]
        [InlineData("C")]
        public void CheckProductsWithDiscount_Test(string ProductName)
        {
            //Arrange
            var product = service.GetRegisteredProducts().FirstOrDefault(p => p.ProductName == ProductName);
            var products = new List<Product>();
            for (int i = 0; i < product.RequiredAmountForDiscount; i++)
                products.Add((Product)product.Clone());

            //Act
            var hasDiscount = DiscountChecker<Product>.IsProductDiscounted(product, products);

            //Assert
            Assert.True(hasDiscount);
        }

        [Theory]
        [InlineData("B")]
        [InlineData("D")]
        public void CheckProductsWithoutDiscount_Test(string ProductName)
        {
            //Arrange
            var product = service.GetRegisteredProducts().FirstOrDefault(p => p.ProductName == ProductName);
            var products = new List<Product>() { (Product)product.Clone() };


            //Act
            var hasDiscount = DiscountChecker<Product>.IsProductDiscounted(product, products);

            //Assert
            Assert.False(hasDiscount);
        }

        [Fact]
        public void CheckInvalidRequiredAmountOfProduct_Test()
        {
            //Arrange
            var product = service.GetRegisteredProducts().FirstOrDefault(p => p.ProductName == "A");
            var products = new List<Product>() { (Product)product.Clone() };

            //Act
            var hasDiscount = DiscountChecker<Product>.IsProductDiscounted(product, products);

            //Assert
            Assert.False(hasDiscount);
        }
    }
}
