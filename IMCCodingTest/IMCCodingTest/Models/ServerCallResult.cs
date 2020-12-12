using System;
using System.Collections.Generic;
using System.Text;

namespace IMCCodingTest.Models
{
    /// <summary>
    /// Model class to show return values from server
    /// </summary>
    public class ServerCallResult
    {
        public string InputString { get; set; }

        public string OutputString { get; set; }

        public string ResponseCode { get; set; }

        public string ResponsePhrase { get; set; }

    }
}
