using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IMCCodingTest.Models;

namespace IMCCodingTest.Interfaces
{
    /// <summary>
    /// Interface that Calculators must abide by
    /// </summary>
    public interface ICalculator
    {
        Task<ServerCallResult> CalculateTaxes(Order targetOrder);

        Task<ServerCallResult> GetTaxRatesForLocation(Location targetLocation);
    }
}
