using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IMCCodingTest.Calculators;
using IMCCodingTest.Models;
using System.Threading.Tasks;

namespace IMCCodingTestUnitTests
{
    /// <summary>
    /// Tests to exercise the Tax Calculator's branches
    /// 
    /// Note: Not strictly Unit Tests, as they rely upon an external server to operate, but the emailed answer I got seemed to indicate I was to test the service's responses.
    /// Note: Depend on the key in IMCCodingTests.DemoConstants, which has a material cost
    /// </summary>
    [TestClass]
    public class TaxJarCalculatorTests
    {

        /// <summary>
        /// Verify null input fails properly
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestCalculateTaxesWithNullOrder()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            ServerCallResult result = await t.CalculateTaxes(null);
            Assert.AreEqual("NotAcceptable", result.ResponseCode);
        }

        /// <summary>
        /// Verify empty input fails properly
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CalculateTaxesWithEmptyOrder()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            ServerCallResult result = await t.CalculateTaxes(new Order());
            Assert.AreEqual("BadRequest", result.ResponseCode);
        }

        /// <summary>
        /// Verify proper input succeeds as expected
        /// Note: in case of failure examine https://developers.taxjar.com/api/reference/ and make certain that the demo info hasn't changed
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CalculateTaxesWithProperInput()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            Order testOrder = GenerateProperOrderWithFromAndToLocations();

            ServerCallResult result = await t.CalculateTaxes(testOrder);
            Assert.AreEqual("OK", result.ResponseCode);
        }

        /// <summary>
        /// Verify proper discrimination for bad input
        /// Note: In case of failure examine manually to make certain this isn't simply a format change in the services message
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CalculateTaxesWithImproperState()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            Order testOrder = GenerateProperOrderWithFromAndToLocations();

            testOrder.to_state = "XX";

            ServerCallResult result = await t.CalculateTaxes(testOrder);
            Assert.AreEqual("BadRequest", result.ResponseCode);

            string template = "to_zip 10541 is not used within to_state XX";

            Assert.IsTrue(result.OutputString.Contains(template));
        }

        /// <summary>
        /// Verify that service is properly handling nexus location input
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CalculateTaxesWithNexusAddresses()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            Order testOrder = GenerateProperOrderWithNexusLocations();

            ServerCallResult result = await t.CalculateTaxes(testOrder);
            Assert.AreEqual("OK", result.ResponseCode);
        }

        /// <summary>
        /// Exercise GetTaxRateForLocation with only zip
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTaxRateForLocation()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            Location testLocation = new Location();
            testLocation.zip = "05495-2086";

            ServerCallResult result = await t.GetTaxRatesForLocation(testLocation);
            Assert.AreEqual("OK", result.ResponseCode);
        }

        /// <summary>
        /// Exercise GetTaxRateForLocation with all parameters
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTaxRateForLocationWithParameters()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            Location testLocation = GenerateLocation();

            ServerCallResult result = await t.GetTaxRatesForLocation(testLocation);
            Assert.AreEqual("OK", result.ResponseCode);
        }

        /// <summary>
        /// Verify GetTaxRateForLocation fails properly without mandatory parameter
        /// Note: If this test breaks, manually examine to make sure it isn't simply an error message change on the server
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetTaxRateForUSLocationNoZip()
        {
            TaxJarCalculator t = new TaxJarCalculator();
            Location testLocation = GenerateLocation();
            testLocation.zip = string.Empty;

            ServerCallResult result = await t.GetTaxRatesForLocation(testLocation);
            Assert.AreEqual("BadRequest", result.ResponseCode);

            string template = "No zip";
            Assert.IsTrue(result.OutputString.Contains(template));
        }

        /// <summary>
        /// Helper method to generate proper to/from Order
        /// </summary>
        /// <returns></returns>
        public Order GenerateProperOrderWithFromAndToLocations()
        {
            Order returnValue = new Order("US", "10118", "NY", "New York", "350 5th Avenue", "US", "10541", "NY", "Mahopac", "668 Route Six", 19.95f, 10);
            LineItem temp = new LineItem("1", 1, "20010", 19.95f, 0);
            returnValue.line_items.Add(temp);
            return returnValue;
        }

        /// <summary>
        /// Helper method to generate proper order with NexusAddress
        /// </summary>
        /// <returns></returns>
        public Order GenerateProperOrderWithNexusLocations()
        {
            Order returnValue = new Order("US", "92093", "CA", "La Jolla", "9500 Gilman Drive", "US", "10541", "NY", "Mahopac", "668 Route Six", 19.95f, 10);

            LineItem tempLineItem = new LineItem("1", 1, "20010", 15f, 0);
            returnValue.line_items.Add(tempLineItem);

            NexusAddress tempAddress = new NexusAddress("Main Location", "US", "92093", "CA", "La Jolla", "9500 Gilman Drive");
            returnValue.nexus_addresses.Add(tempAddress);

            return returnValue;
        }

        /// <summary>
        /// Helper method to generate a valid Location
        /// </summary>
        /// <returns></returns>
        public Location GenerateLocation()
        {
            Location returnValue = new Location("US", "05495-2086", "VT", "Williston", "312 Hurricane Lane");
            return returnValue;
        }
    }
}
