using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using IMCCodingTest.Services;
using IMCCodingTest.Models;
using IMCCodingTest.Interfaces;
using IMCCodingTest.Calculators;

namespace IMCCodingTestUnitTests
{
    /// <summary>
    /// Tests to interrogate IMCCodingTest TaxService.  The target is primarily a passthrough, but will utimately have functionality that
    /// revolves around selecting the proper Calculator for each query.
    /// </summary>
    [TestClass]
    public class TaxServiceTests
    {
        /// <summary>
        /// Verify proper Calculator returned for Location
        /// Testing a stub method, will always be TaxJarCalculator
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestGetProperCalculatorForLocation()
        {
            TaxService ts = TaxService.Singleton;
            Location tempLocation = new Location();
            ICalculator calc = ts.GetCalculator(tempLocation);
            Assert.IsTrue(calc is TaxJarCalculator);
        }

        /// <summary>
        /// Verify proper Calculator returned for Order
        /// Testing a Stub method, will always be TaxJarCalculator
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestGetProperCalculatorForOrder()
        {
            TaxService ts = TaxService.Singleton;
            Order tempOrder = new Order();
            ICalculator calc = ts.GetCalculator(tempOrder);
            Assert.IsTrue(calc is TaxJarCalculator);
        }
    }
}
