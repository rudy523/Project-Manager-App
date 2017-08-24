using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace ProjectManager.Model
{
    public class LoadTDL
    {
        #region Properties
        // Collection holding all TDL data from source File
        public IEnumerable<XElement> RawTDLs { get; set; }



        #endregion
        #region Constructors
        // This constructor gets string input from the TDL search bar and uses it to filter RawTDLs
        public LoadTDL(string[] input)
        {


            string FinancialFeed = @"\\Scrdata\bah\Rudy\Workbook Development\Programming\Financial Workbook TDLs.xml";
            XElement Root = XElement.Load(FinancialFeed);


        }
        #endregion
        #region Methods
        private void LoadSourceData()
        {
            // Sets location of source data file
            string FinancialFeed = @"\\Scrdata\bah\Rudy\Workbook Development\Programming\Financial Workbook TDLs.xml";
            // Loads source data into XElement. This will be the source to run queries against. NOTE: May require conversion to IEnumberable.
            XElement Root = XElement.Load(FinancialFeed);
        }


        #endregion

    }
}
