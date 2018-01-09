using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    public class TDLViewModel : ObservableCollection<KeyValuePair<string, decimal>>
    {
        #region Properties
        public string TDL_No { get; set; }
        public string Contract { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompDate { get; set; }
        public string Engineer { get; set; }
        public string Subcontractor { get; set; }
        public DateTime WeekEnding { get; set; }
        public decimal BudgHrs { get; set; }
        public decimal BudgLab { get; set; }
        public decimal BudgTrv { get; set; }
        public decimal BudgMat { get; set; }
        public decimal BudgOther { get; set; }
        public decimal BudgTotal { get; set; }
        public decimal FundLab { get; set; }
        public decimal FundTrv { get; set; }
        public decimal FundMat { get; set; }
        public decimal FundOther { get; set; }
        public decimal FundTotal { get; set; }
        public decimal ExpHours { get; set; }
        public decimal ExpLab { get; set; }
        public decimal ExpTrv { get; set; }
        public decimal ExpMat { get; set; }
        public decimal ExpOther { get; set; }
        public decimal ExpTotal { get; set; }
        public decimal BudgBalHrs { get; set; }
        public decimal BudgBalLab { get; set; }
        public decimal BudgBalTrv { get; set; }
        public decimal BudgBalMat { get; set; }
        public decimal BudgBalOther { get; set; }
        public decimal BudgBalTotal { get; set; }
        public decimal FundBalLab { get; set; }
        public decimal FundBalTrv { get; set; }
        public decimal FundBalMat { get; set; }
        public decimal FundBalOther { get; set; }
        public decimal FundBalTotal { get; set; }

        #endregion

        #region Constructors
        // Blank constructor for serialization purposes
        public TDLViewModel() { }
        // Normal constructor that takes a string array passed in from raw XML data and assigns to properties
        public TDLViewModel(XElement TDL)
        {
            var input = TDL.Attributes().ToList();

            switch (input.Count())
            {
                case 37:
                    if (input.ElementAt(6).Name == "SUBCONTRACTOR")                 
                    {
                        XAttribute ExpendedAs = new XAttribute("WEEK_ENDING_DATE", "1/1/1900");
                        input.Insert(7, ExpendedAs);
                    }
                    if (input.ElementAt(6).Name == "WEEK_ENDING_DATE")
                    {
                        XAttribute Sub = new XAttribute((XName)"SUBCONTRACTOR", "N/A");
                        input.Insert(6, Sub);
                    }
                    break;
                case 36:
                    XAttribute Subcontractor = new XAttribute((XName)"SUBCONTRACTOR", "N/A");
                    input.Insert(6, Subcontractor);
                    XAttribute ExpendedAsOf = new XAttribute((XName)"WEEK_ENDING_DATE", "1/1/1900");
                    input.Insert(7, ExpendedAsOf);
                    break;
                default:
                    break;
            }

            input.Remove(input.ElementAt(14));

            if (input.ElementAt(0) != null)
            {
                TDL_No = input.ElementAt(0).Value.ToString();
            }
            else TDL_No = "NULL";

            if (input.ElementAt(1) != null)
            {
                Contract = input.ElementAt(1).Value.ToString();
            }
            else Contract = "NULL";

            if (input.ElementAt(2) != null)
            {
                Description = input.ElementAt(2).Value.ToString(); 
            }
            else Description = "NULL";

            if (input.ElementAt(3) != null)
            {
                StartDate = DateTime.Parse(input.ElementAt(3).Value.ToString());
            }
            else StartDate = DateTime.Parse("1/1/2000");

            if (input.ElementAt(4) != null)
            {
                CompDate = DateTime.Parse(input.ElementAt(4).Value.ToString());
            }
            else CompDate = DateTime.Parse("1/1/2000");

            if (input.ElementAt(5) != null)
            {
                Engineer = input.ElementAt(5).Value.ToString();
            }
            else Engineer = "NULL";

            if (input.ElementAt(6) != null)
            {
                Subcontractor = input.ElementAt(6).Value.ToString();
            }
            else Subcontractor = "NULL";

            if (input.ElementAt(7) != null)
            {
                WeekEnding = DateTime.Parse(input.ElementAt(7).Value.ToString());
            }
            else CompDate = DateTime.Parse("1/1/2000");

            if (input.ElementAt(8) != null)
            {
                BudgHrs = decimal.Parse(input.ElementAt(8).Value.ToString());
            }
            else BudgHrs = 0.00M;

            if (input.ElementAt(9) != null)
            {
                BudgLab = decimal.Parse(input.ElementAt(9).Value.ToString());
            }
            else BudgLab = 0.00M;

            if (input.ElementAt(10) != null)
            {
                BudgTrv = decimal.Parse(input.ElementAt(10).Value.ToString());
            }
            else BudgTrv = 0.00M;

            if (input.ElementAt(11) != null)
            {
                BudgMat = decimal.Parse(input.ElementAt(11).Value.ToString());
            }
            else BudgMat= 0.00M;

            if (input.ElementAt(12) != null)
            {
                BudgOther = decimal.Parse(input.ElementAt(12).Value.ToString());
            }
            else BudgOther = 0.00M;

            if (input.ElementAt(13) != null)
            {
                BudgTotal = decimal.Parse(input.ElementAt(13).Value.ToString());
            }
            else BudgTotal = 0.00M;

            if (input.ElementAt(14) != null)
            {
                FundLab = decimal.Parse(input.ElementAt(14).Value.ToString());
            }
            else FundLab = 0.00M;

            if (input.ElementAt(15) != null)
            {
                FundTrv = decimal.Parse(input.ElementAt(15).Value.ToString());
            }
            else FundTrv = 0.00M;

            if (input.ElementAt(16) != null)
            {
                FundMat = decimal.Parse(input.ElementAt(16).Value.ToString());
            }
            else FundMat = 0.00M;

            if (input.ElementAt(17) != null)
            {
                FundOther = decimal.Parse(input.ElementAt(17).Value.ToString());
            }
            else FundOther = 0.00M;

            if (input.ElementAt(18) != null)
            {
                FundTotal = decimal.Parse(input.ElementAt(18).Value.ToString());
            }
            else FundTotal = 0.00M;

            if (input.ElementAt(19) != null)
            {
                ExpHours = decimal.Parse(input.ElementAt(19).Value.ToString());
            }
            else ExpHours = 0.00M;

            if (input.ElementAt(20) != null)
            {
                ExpLab = decimal.Parse(input.ElementAt(20).Value.ToString());
            }
            else ExpLab = 0.00M;

            if (input.ElementAt(21) != null)
            {
                ExpTrv = decimal.Parse(input.ElementAt(21).Value.ToString());
            }
            else ExpTrv = 0.00M;

            if (input.ElementAt(22) != null)
            {
                ExpMat = decimal.Parse(input.ElementAt(22).Value.ToString());
            }
            else ExpMat = 0.00M;

            if (input.ElementAt(23) != null)
            {
                ExpOther = decimal.Parse(input.ElementAt(23).Value.ToString());
            }
            else ExpOther = 0.00M;

            if (input.ElementAt(24) != null)
            {
                ExpTotal = decimal.Parse(input.ElementAt(24).Value.ToString());
            }
            else ExpTotal = 0.00M;

            if (input.ElementAt(25) != null)
            {
                BudgBalHrs = decimal.Parse(input.ElementAt(25).Value.ToString());
            }
            else BudgBalHrs = 0.00M;

            if (input.ElementAt(26) != null)
            {
                BudgBalLab = decimal.Parse(input.ElementAt(26).Value.ToString());
            }
            else BudgBalLab = 0.00M;

            if (input.ElementAt(27) != null)
            {
                BudgBalTrv = decimal.Parse(input.ElementAt(27).Value.ToString());
            }
            else BudgBalTrv = 0.00M;

            if (input.ElementAt(28) != null)
            {
                BudgBalMat = decimal.Parse(input.ElementAt(28).Value.ToString());
            }
            else BudgBalMat = 0.00M;

            if (input.ElementAt(29) != null)
            {
                BudgBalOther = decimal.Parse(input.ElementAt(29).Value.ToString());
            }
            else BudgBalOther = 0.00M;

            if (input.ElementAt(30) != null)
            {
                BudgBalTotal = decimal.Parse(input.ElementAt(30).Value.ToString());
            }
            else BudgBalTotal = 0.00M;

            if (input.ElementAt(31) != null)
            {
                FundBalLab = decimal.Parse(input.ElementAt(31).Value.ToString());
            }
            else FundBalLab = 0.00M;

            if (input.ElementAt(32) != null)
            {
               FundBalTrv = decimal.Parse(input.ElementAt(32).Value.ToString());
            }
            else FundBalTrv = 0.00M;

            if (input.ElementAt(33) != null)
            {
                FundBalMat = decimal.Parse(input.ElementAt(33).Value.ToString());
            }
            else FundBalMat = 0.00M;

            if (input.ElementAt(34) != null)
            {
                FundBalOther = decimal.Parse(input.ElementAt(34).Value.ToString());
            }
            else FundBalOther = 0.00M;

            if (input.ElementAt(35) != null)
            {
                FundBalTotal = decimal.Parse(input.ElementAt(35).Value.ToString());
            }
            else FundBalTotal = 0.00M;

        }

        public void AddItems(string input)
        {
            Clear();

            switch (input)
            {
                case "Labor":
                    // Labor
                    Add(new KeyValuePair<string, decimal>("Budget - Labor", BudgLab));
                    Add(new KeyValuePair<string, decimal>("Funded - Labor", FundLab));
                    Add(new KeyValuePair<string, decimal>("Expended - Labor", ExpLab));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Labor", FundBalLab));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Labor", BudgBalLab));
                    break;
                case "Travel":
                    // Travel
                    Add(new KeyValuePair<string, decimal>("Budget - Travel", BudgTrv));
                    Add(new KeyValuePair<string, decimal>("Funded - Travel", FundTrv));
                    Add(new KeyValuePair<string, decimal>("Expended - Travel", ExpTrv));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Travel", FundBalTrv));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Travel", BudgBalTrv));
                    break;
                case "Material":
                    // Material
                    Add(new KeyValuePair<string, decimal>("Budget - Material", BudgMat));
                    Add(new KeyValuePair<string, decimal>("Funded - Material", FundMat));
                    Add(new KeyValuePair<string, decimal>("Expended - Material", ExpMat));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Material", FundBalMat));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Material", BudgBalMat));
                    break;
                case "Totals":
                    // Totals 
                    Add(new KeyValuePair<string, decimal>("Budget - Total", BudgTotal));
                    Add(new KeyValuePair<string, decimal>("Funded - Total", FundTotal));
                    Add(new KeyValuePair<string, decimal>("Expended - Total", ExpTotal));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Total", FundBalTotal));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Total", BudgBalTotal));
                    break;
                case "All":
                    // Labor
                    Add(new KeyValuePair<string, decimal>("Budget - Labor", BudgLab));
                    Add(new KeyValuePair<string, decimal>("Funded - Labor", FundLab));
                    Add(new KeyValuePair<string, decimal>("Expended - Labor", ExpLab));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Labor", FundBalLab));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Labor", BudgBalLab)); 
                    // Travel
                    Add(new KeyValuePair<string, decimal>("Budget - Travel", BudgTrv));
                    Add(new KeyValuePair<string, decimal>("Funded - Travel", FundTrv));
                    Add(new KeyValuePair<string, decimal>("Expended - Travel", ExpTrv));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Travel", FundBalTrv));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Travel", BudgBalTrv));      
                    // Material
                    Add(new KeyValuePair<string, decimal>("Budget - Material", BudgMat));
                    Add(new KeyValuePair<string, decimal>("Funded - Material", FundMat));
                    Add(new KeyValuePair<string, decimal>("Expended - Material", ExpMat));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Material", FundBalMat));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Material", BudgBalMat));          
                    // Totals 
                    Add(new KeyValuePair<string, decimal>("Budget - Total", BudgTotal));
                    Add(new KeyValuePair<string, decimal>("Funded - Total", FundTotal));
                    Add(new KeyValuePair<string, decimal>("Expended - Total", ExpTotal));
                    Add(new KeyValuePair<string, decimal>("Funds Remaining - Total", FundBalTotal));
                    Add(new KeyValuePair<string, decimal>("Budget Remaining - Total", BudgBalTotal));
                    break;
                default:
                    break;
            }
        }
        private void clearValues()
        {
            Clear();
        }
        #endregion
    }
}
