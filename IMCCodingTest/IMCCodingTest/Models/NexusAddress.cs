using System;
using System.Collections.Generic;
using System.Text;

namespace IMCCodingTest.Models
{
    /// <summary>
    /// Model class to store NexusAddress info
    /// Note: Do not change param names, relying on serializer for server communication
    /// </summary>
    public class NexusAddress
    {
        public NexusAddress()
        {

        }

        public NexusAddress(string tid, string tcountry, string tzip, string tstate, string tcity, string tstreet)
        {
            id = tid;
            country = tcountry;
            zip = tzip;
            state = tstate;
            city = tcity;
            street = tstreet;
        }
        public string id { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string street { get; set; }
    }
}
