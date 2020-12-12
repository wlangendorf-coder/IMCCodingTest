using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;

namespace IMCCodingTest.Models
{
    /// <summary>
    /// Model class to store order info
    /// Note: Do not change param names, relying on serializer for server communication
    /// </summary>
    public class Order
    {
        public Order()
        {
            nexus_addresses = new List<NexusAddress>();
            line_items = new List<LineItem>();
        }

        public Order(string fcountry, string fzip, string fstate, string fcity, string fstreet,
            string tcountry, string tzip, string tstate, string tcity, string tstreet,
            float iamount, float ishipping) : this()
        {
            from_country = fcountry;
            from_zip = fzip;
            from_state = fstate;
            from_city = fcity;
            from_street = fstreet;

            to_country = tcountry;
            to_zip = tzip;
            to_state = tstate;
            to_city = tcity;
            to_street = tstreet;

            amount = iamount;
            shipping = ishipping;
        }
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string from_city { get; set; }
        public string from_street { get; set; }

        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }

        public string to_city { get; set; }

        public string to_street { get; set; }

        public float amount { get; set; }

        public float shipping { get; set; }
        
        public List<NexusAddress> nexus_addresses { get; set; }

        public List<LineItem> line_items { get; set; }
    }
}
