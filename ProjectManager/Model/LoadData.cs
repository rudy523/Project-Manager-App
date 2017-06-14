using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using ProjectManager;

namespace ProjectManager.Model
{
    public class LoadData
    {
        public string FinancialFeed { get; set; }
        public string TaskFeed { get; set; }
        public XElement FinanceRoot { get; set; }
        public XElement TaskRoot { get; set; }
        public List<string> DistinctMIPRs { get; set; }
        public IEnumerable<XElement> ProjectNumbers { get; set; }
        public IEnumerable<XAttribute> ProjNumDetails { get; set; }
        public ObservableCollection<MIPRNumber> ProjectNumbersView { get; set; }
        public ObservableCollection<MIPR> MIPRcollection { get; set; }

        public LoadData(string[] input)
        {
            GetViewProjectNumbers(input);
        }

        private void GetFinancialData()
        {
            FinancialFeed = @"\\Scrdata\bah\Rudy\Workbook Development\Programming\Financial Workbook Project Totals.xml";
            this.FinanceRoot = XElement.Load(FinancialFeed);
            this.ProjectNumbers = FinanceRoot.Descendants();
            this.ProjNumDetails = ProjectNumbers.Attributes();
            this.ProjectNumbersView = new ObservableCollection<MIPRNumber>();
            this.DistinctMIPRs = new List<string>();
            this.MIPRcollection = new ObservableCollection<MIPR>();
        }

        private void GetProjectNumberDetails(IEnumerable<XAttribute> source, List<string> output)
        {
            if (source.Count() == 41)
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    string value = source.ElementAt(i).Value.ToString();
                    output.Add(value);
                }
            }
            else if (source.Count() == 40)
            {
                for (int n = 0; n < 40; n++)
                {
                    string value = source.ElementAt(n).Value.ToString();
                    output.Add(value);
                }
                output.Insert(5, "0");
            }
            else
            {
                throw new IndexOutOfRangeException();
            }          
        }

        private MIPRNumber CreateProjectNumber(List<string> input)
        {
            MIPRNumber num = new MIPRNumber(input.ElementAt(0), input.ElementAt(1), input.ElementAt(2), input.ElementAt(3), input.ElementAt(4), input.ElementAt(5),
                input.ElementAt(6), input.ElementAt(7), input.ElementAt(8), input.ElementAt(9), input.ElementAt(10), input.ElementAt(11), input.ElementAt(12), input.ElementAt(13),
                decimal.Parse(input.ElementAt(14)), decimal.Parse(input.ElementAt(15)), decimal.Parse(input.ElementAt(16)), decimal.Parse(input.ElementAt(17)), decimal.Parse(input.ElementAt(18)),
                decimal.Parse(input.ElementAt(19)), decimal.Parse(input.ElementAt(20)), decimal.Parse(input.ElementAt(21)), decimal.Parse(input.ElementAt(22)), decimal.Parse(input.ElementAt(23)),
                decimal.Parse(input.ElementAt(24)), decimal.Parse(input.ElementAt(25)), decimal.Parse(input.ElementAt(26)), decimal.Parse(input.ElementAt(27)), decimal.Parse(input.ElementAt(28)),
                decimal.Parse(input.ElementAt(29)), decimal.Parse(input.ElementAt(30)), decimal.Parse(input.ElementAt(31)), decimal.Parse(input.ElementAt(32)), decimal.Parse(input.ElementAt(33)),
                decimal.Parse(input.ElementAt(34)), decimal.Parse(input.ElementAt(35)), decimal.Parse(input.ElementAt(36)), decimal.Parse(input.ElementAt(37)), DateTime.Parse(input.ElementAt(38)),
                DateTime.Parse(input.ElementAt(39)), DateTime.Parse(input.ElementAt(40)));

            return num;
        }

        private void DistinctMIPR()
        {
            foreach (var item in ProjectNumbersView)
            {
                DistinctMIPRs.Add(item.MIPRnum);
            }
            IEnumerable<string> nums = DistinctMIPRs.Distinct();
            DistinctMIPRs = nums.ToList();
        }

        private void GetMIPRcollection()
        {
            foreach (var item in this.DistinctMIPRs)
            {
                IEnumerable<MIPRNumber> matches = from match in this.ProjectNumbersView
                                                  where match.MIPRnum == item
                                                  select match;
                ObservableCollection<ProjectNumber> ProjNum = new ObservableCollection<ProjectNumber>();
                foreach (var num in matches)
                {
                    ObservableCollection<string> info = new ObservableCollection<string>
                    {
                        num.BillingElem,
                        num.Network,
                        num.Activity,
                        num.SubElem,
                        num.Appn,
                        num.AppnNo,
                        num.DocType,
                        num.Program,
                        num.Project,
                        num.Title,
                        num.Sponsor,
                        num.Engineer
                    };
                    string LabAlloc = string.Format("{0:C}", num.LabAlloc);
                    info.Add(LabAlloc);
                    string MatAlloc = string.Format("{0:C}", num.MatAlloc);
                    info.Add(MatAlloc);
                    string TrvAlloc = string.Format("{0:C}", num.TrvAlloc);
                    info.Add(TrvAlloc);
                    string SvcAlloc = string.Format("{0:C}", num.SvcAlloc);
                    info.Add(SvcAlloc);
                    string DivAlloc = string.Format("{0:C}", num.DivAlloc);
                    info.Add(DivAlloc);
                    string CBAlloc = string.Format("{0:C}", num.CBAlloc);
                    info.Add(CBAlloc);
                    string OtherAlloc = string.Format("{0:C}", num.OtherAlloc);
                    info.Add(OtherAlloc);
                    string TotalAlloc = string.Format("{0:C}", num.TotalAlloc);
                    info.Add(TotalAlloc);
                    string LabExp = string.Format("{0:C}", num.LabExp);
                    info.Add(LabExp);
                    string MatExp = string.Format("{0:C}", num.MatExp);
                    info.Add(MatExp);
                    string TrvExp = string.Format("{0:C}", num.TrvExp);
                    info.Add(TrvExp);
                    string SvcExp = string.Format("{0:C}", num.SvcExp);
                    info.Add(SvcExp);
                    string DivExp = string.Format("{0:C}", num.DivExp);
                    info.Add(DivExp);
                    string CBExp = string.Format("{0:C}", num.CBExp);
                    info.Add(CBExp);
                    string OtherExp = string.Format("{0:C}", num.OtherExp);
                    info.Add(OtherExp);
                    string TotalExp = string.Format("{0:C}", num.TotalExp);
                    info.Add(TotalExp);
                    string LabBal = string.Format("{0:C}", num.LabBal);
                    info.Add(LabBal);
                    string MatBal = string.Format("{0:C}", num.MatBal);
                    info.Add(MatBal);
                    string TrvBal = string.Format("{0:C}", num.TrvBal);
                    info.Add(TrvBal);
                    string SvcBal = string.Format("{0:C}", num.SvcBal);
                    info.Add(SvcBal);
                    string DivBal = string.Format("{0:C}", num.DivBal);
                    info.Add(DivBal);
                    string CBBal = string.Format("{0:C}", num.CBBal);
                    info.Add(CBBal);
                    string OtherBal = string.Format("{0:C}", num.OtherBal);
                    info.Add(OtherBal);
                    string TotalBal = string.Format("{0:C}", num.TotalBal);
                    info.Add(TotalBal);
                    info.Add(num.WCD.ToString());
                    info.Add(num.AppnExp.ToString());
                    info.Add(num.AcceptDate.ToString());
                    ProjectNumber number = new ProjectNumber(num.Projnum, info);
                    ProjNum.Add(number);
                }              
                MIPR mipr = new MIPR(item, ProjNum);
                this.MIPRcollection.Add(mipr);
            }
        }
        
        private void GetViewProjectNumbers(string[] criteria)
        {
            GetFinancialData();

            foreach (var item in criteria)
            {
                IEnumerable<XElement> matches = from match in ProjNumDetails
                                                where match.Value == item
                                                select match.Parent;

                foreach (var elem in matches)
                {
                    IEnumerable<XAttribute> rawData = elem.Attributes();

                    if (rawData.Count() < 40)
                    {
                        continue;
                    }

                    List<string> Data = new List<string>();

                    GetProjectNumberDetails(rawData, Data);

                    MIPRNumber num = CreateProjectNumber(Data);

                    this.ProjectNumbersView.Add(num);
                }

                DistinctMIPR();

                GetMIPRcollection();
            }

        }

    }
}
