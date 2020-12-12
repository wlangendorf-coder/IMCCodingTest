using System;
using System.Collections.Generic;
using System.Text;

namespace IMCCodingTest.Models
{
    /// <summary>
    /// Model for storing LineItem data
    /// Note: Do not change param names, relying on serializer for server communication
    /// </summary>
    public class LineItem
    {
        public LineItem()
        {

        }
        public LineItem(string tid, int tquantity, string tproduct_tax_code, float tunit_price, float tdiscount)
        {
            id = tid;
            quantity = tquantity;
            product_tax_code = tproduct_tax_code;
            unit_price = tunit_price;
            discount = tdiscount;
        }

        public string id { get; set; }
        public int quantity { get; set; }
        public string product_tax_code { get; set; }
        public float unit_price { get; set; }
        public float discount { get; set; }
    }
}
