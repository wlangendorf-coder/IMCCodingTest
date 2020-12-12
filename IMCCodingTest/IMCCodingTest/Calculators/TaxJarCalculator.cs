using System;
using System.Text;
using IMCCodingTest.Models;
using IMCCodingTest.Interfaces;
using System.Net.Http;
using IMCCodingTest.Constants;
using System.Threading.Tasks;
using System.Text.Json;

namespace IMCCodingTest.Calculators
{
    /// <summary>
    /// Calculator which calls the API described in https://developers.taxjar.com/api/reference/, uses httpclient
    /// </summary>
    public class TaxJarCalculator : ICalculator
    {

        /// <summary>
        /// Calls taxjar api's 'taxes' endpoint, with the information provided in the targetOrder parameter
        /// </summary>
        /// <param name="targetOrder"></param>
        /// <returns>A ServiceCallResult with the data from the response from the service</returns>
        public async Task<ServerCallResult> CalculateTaxes(Order targetOrder)
        {
            ServerCallResult returnValue = new ServerCallResult();
            try
            {
                string text = JsonSerializer.Serialize(targetOrder);
                returnValue.InputString = text;
                StringContent stringContent = new StringContent(text, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization","Bearer "+DemoConstants.TaxJarKey);
                HttpResponseMessage response = await client.PostAsync(DemoConstants.TaxJarAPIEndpoint+"taxes", stringContent);
                string responseBody = await response.Content.ReadAsStringAsync();

                returnValue.OutputString = responseBody;
                returnValue.ResponseCode = response.StatusCode.ToString();
                returnValue.ResponsePhrase = response.ReasonPhrase;
            }
            catch (Exception e) 
            {
                returnValue.ResponseCode = "Client Exception";
                returnValue.ResponsePhrase = e.Message;
            }
            
            return returnValue;
        }

        /// <summary>
        /// Calls TaxJar's 'rates' endpoint with the data from targetLocation
        /// </summary>
        /// <param name="targetLocation"></param>
        /// <returns>A ServiceCallResult with the data from the response from the service</returns>
        public async Task<ServerCallResult> GetTaxRatesForLocation(Location targetLocation)
        {
            ServerCallResult returnValue = new ServerCallResult();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", "Bearer " + DemoConstants.TaxJarKey);

                string paramString = GenerateURLEndingForLocation(targetLocation);

                HttpResponseMessage response = await client.GetAsync(DemoConstants.TaxJarAPIEndpoint + "rates/"+paramString);
                string responseBody = await response.Content.ReadAsStringAsync();

                returnValue.OutputString = responseBody;
                returnValue.ResponseCode = response.StatusCode.ToString();
                returnValue.ResponsePhrase = response.ReasonPhrase;
            }
            catch (Exception e)
            {
                returnValue.ResponseCode = "Client Exception";
                returnValue.ResponsePhrase = e.Message;
            }

            return returnValue;
        }

        /// <summary>
        /// Helper method to make the url for GetTaxRatesForLocation
        /// I'm like 80% sure that this can be done by a one line call or two line to url, format, or something similar, but I couldn't find it,
        /// so I did it manually.
        /// </summary>
        /// <param name="targetLocation"></param>
        /// <returns></returns>
        private string GenerateURLEndingForLocation(Location targetLocation)
        {
            bool FirstParam = true;
            string returnValue = string.Empty;
            returnValue = returnValue + targetLocation.zip;
            if(!string.IsNullOrEmpty(targetLocation.country))
            {
                if(FirstParam)
                {
                    returnValue = returnValue + "?country=" + targetLocation.country;
                    FirstParam = false;
                }
            }
            if (!string.IsNullOrEmpty(targetLocation.country))
            {
                if (FirstParam)
                {
                    returnValue = returnValue + "?state=" + targetLocation.state;
                    FirstParam = false;
                }
                else
                {
                    returnValue = returnValue + "&state=" + targetLocation.state;
                }
            }
            if (!string.IsNullOrEmpty(targetLocation.country))
            {
                if (FirstParam)
                {
                    returnValue = returnValue + "?city=" + targetLocation.city;
                    FirstParam = false;
                }
                else
                {
                    returnValue = returnValue + "&city=" + targetLocation.city;
                }
            }
            if (!string.IsNullOrEmpty(targetLocation.country))
            {
                if (FirstParam)
                {
                    returnValue = returnValue + "?street=" + targetLocation.street;
                    FirstParam = false;
                }
                else
                {
                    returnValue = returnValue + "&street=" + targetLocation.street;
                }
            }
            return returnValue;
        }
    }
}
