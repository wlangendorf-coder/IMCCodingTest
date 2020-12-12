using System;
using System.Collections.Generic;
using System.Text;

namespace IMCCodingTest.Models
{
    /// <summary>
    /// Model for storing Location data
    /// Note: Do not change param names, relying on serializer for server communication
    /// </summary>
    public class Location
    {
        public Location()
        {

        }
        public Location(string tcountry, string tzip, string tstate, string tcity, string tstreet)
        {
            country = tcountry;
            zip = tzip;
            state = tstate;
            city = tcity;
            street = tstreet;
        }
        public string country;
        public string zip;
        public string state;
        public string city;
        public string street;
    }
}
