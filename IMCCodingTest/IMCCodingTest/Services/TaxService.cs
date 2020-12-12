using System;
using System.Collections.Generic;
using System.Text;
using IMCCodingTest.Interfaces;
using IMCCodingTest.Models;
using IMCCodingTest.Calculators;
using System.Threading.Tasks;

namespace IMCCodingTest.Services
{
    /// <summary>
    /// Tax Service, used to communicate with the various tax API's
    /// Presently essentially a data passthrough to TaxJarCalculator, but in theory a routing station to choose the proper Calculator
    /// </summary>
    public class TaxService
    {
        #region variables
        /// <summary>
        /// private TaxService to power the singleton
        /// </summary>
        private static TaxService instance;
        public ICalculator Calculator;
        #endregion
        /// <summary>
        /// Private constructor
        /// </summary>
        private TaxService()
        {
        }

        /// <summary>
        /// We only want one service to exist at a time, so we use a Singleton.
        /// </summary>
        public static TaxService Singleton
        {
            get
            {
                if (instance == null)
                {
                    instance = new TaxService();
                }
                return instance;
            }
        }

        /// <summary>
        /// Stub method which always returns TaxJarCalculator, regardless of the Order
        /// </summary>
        /// <param name="targetOrder"></param>
        /// <returns></returns>
        public ICalculator GetCalculator(Order targetOrder)
        {
            return new TaxJarCalculator();
        }

        /// <summary>
        /// Stub method which always returns TaxJarCalculator, regardless of the Location
        /// </summary>
        /// <param name="targetLocation"></param>
        /// <returns></returns>
        public ICalculator GetCalculator(Location targetLocation)
        {
            return new TaxJarCalculator();
        }

        /// <summary>
        /// Calls GetCalculator on input, then passes input to that Calculator's CalculateTaxes
        /// </summary>
        /// <param name="targetOrder"></param>
        /// <returns></returns>
        public async Task<ServerCallResult> CalculateTaxes(Order targetOrder)
        {
            Calculator = GetCalculator(targetOrder);
            return await Calculator.CalculateTaxes(targetOrder);
        }

        /// <summary>
        /// Calls GetCalculator on input, then passes input to that Calculator's GetTaxRatesForLocation
        /// </summary>
        /// <param name="targetLocation"></param>
        /// <returns></returns>
        public async Task<ServerCallResult> GetTaxRatesForLocation(Location targetLocation)
        {
            Calculator = GetCalculator(targetLocation);
            return await Calculator.GetTaxRatesForLocation(targetLocation);
        }

    }
}
