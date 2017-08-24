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
            IEnumerable <XElement> children = FinanceRoot.Descendants();
            IEnumerable<XAttribute> atts = children.ElementAt(5).Attributes();
            Console.WriteLine(FinanceRoot);
            //Console.WriteLine(FinanceRoot);
            Console.ReadLine();

    }
        }
}
