using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Collections.ObjectModel;



namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string FinancialFeed = @"\\Scrdata\bah\Rudy\Workbook Development\Programming\Financial Workbook TDLs.xml";

            XElement FinanceRoot = XElement.Load(FinancialFeed);
            IEnumerable<XNode> Nodes = FinanceRoot.DescendantNodes();
            IEnumerable<XElement> Descendants = FinanceRoot.Descendants();
            IEnumerable<XAttribute> Atts = Descendants.Attributes();
            IEnumerable<XAttribute> EigthAtts = Descendants.ElementAt(8).Attributes();


            Console.WriteLine("8th Descendant: {0}", Descendants.ElementAt(8));
            Console.WriteLine("8th Node: {0}", Nodes.ElementAt(8));
            
            foreach (var item in EigthAtts)
            {
                Console.WriteLine(item);
            }

            foreach (var item in EigthAtts)
            {
                Console.WriteLine(item.Name);
            }

            foreach (var item in EigthAtts)
            {
                Console.WriteLine(item.Value);
            }

            //Console.WriteLine(FinanceRoot);
            Console.ReadLine();

    }
        }
}
