using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _002_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. dataset
            XElement xmlFile = XElement.Load(@"321gone.xml");
            
            // 2. query
            IEnumerable<XElement> xmlData =
                from item in xmlFile.Descendants("Item")
                where (string) item.Attribute("PartNumber") != "872-AA"
                select item;

            // 3. run query
            foreach (XElement item in xmlData)  
                Console.WriteLine(item);

            Console.WriteLine("======================================");

            // 2. query
            xmlFile = XElement.Load(@"321gone.xml");

            IEnumerable<XElement> xmlData2 =
                from item in xmlFile.Descendants("Item")
                where (int) item.Element("Quantity") * (decimal) item.Element("USPrice") > 100
                orderby (string)item.Element("PartNumber")
                select item;

            // 3. run query
            foreach (XElement item in xmlData2)  
                Console.WriteLine(item);
        }
    }
}
