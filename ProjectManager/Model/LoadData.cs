﻿using System;
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
            this.ProjectNumbersView = new ObservableCollection<MIPRNumber>();
            this.DistinctMIPRs = new List<string>();
            this.MIPRcollection = new ObservableCollection<MIPR>();
            GetViewProjectNumbers(input);
        }

        public void UpdateFunding(string [] input)
        {
            GetFinancialData();

            foreach (var item in input)
            {
                IEnumerable<XElement> matches = from match in ProjNumDetails
                                                where match.Value == item
                                                select match.Parent;
                foreach (var elem in matches)
                {
                    IEnumerable<XAttribute> rawData = elem.Attributes();
                    foreach (var att in rawData)
                    {
                        if (att == null)
                        {
                            att.Value = att.Name.ToString();
                        }
                    }
                }
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
                    IEnumerable<XAttribute> attData = elem.Attributes();
                    List<XAttribute> rawData = attData.ToList();

                    List<string> Data = new List<string>();

                    GetProjectNumberDetails(rawData, Data);

                    MIPRNumber num = CreateProjectNumber(Data);

                    this.ProjectNumbersView.Add(num);
                }

                DistinctMIPR();

                GetMIPRcollection();
            }

        }

        private void GetFinancialData()
        {
            FinancialFeed = @"\\Scrdata\bah\Rudy\Workbook Development\Programming\Financial Workbook Project Totals.xml";
            this.FinanceRoot = XElement.Load(FinancialFeed);
            this.ProjectNumbers = FinanceRoot.Descendants();
            this.ProjNumDetails = ProjectNumbers.Attributes();
            DistinctMIPRs.Clear();
            ProjectNumbersView.Clear();
            MIPRcollection.Clear();
        }

        private void GetProjectNumberDetails(List<XAttribute> source, List<string> output)
        {
            switch (source.Count())
            {
                case 41:
                    for (int i = 0; i < source.Count(); i++)
                    {
                        output.Add(source.ElementAt(i).Value);
                    }
                    break;
                case 40:
                    {
                        for (int n = 0; n < 40; n++)
                        {
                            output.Add(source.ElementAt(n).Value);
                        }
                        output.Insert(5, "0");
                    }
                    break;
                default:
                    int difference = 41 - source.Count();
                    for (int i = 0; i < difference; i++)
                    {
                        source.Insert(source.Count(), new XAttribute("NULL", (string)"NULL"));
                    }

                    for (int i = 0; i < 41; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (source.ElementAt(i).Name == "MIPR")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("MIPR", (string)"NULL"));
                                break;
                            case 1:
                                if (source.ElementAt(i).Name == "PROJECT_NO")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("PROJECT_NO", (string)"NULL"));
                                break;
                            case 2:
                                if (source.ElementAt(i).Name == "BILLING_ELEMENT")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("BILLING_ELEMENT", (string)"NULL"));
                                break;
                            case 3:
                                if (source.ElementAt(i).Name == "NETWORK")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("NETWORK", (string)"NULL"));
                                break;
                            case 4:
                                if (source.ElementAt(i).Name == "ACTIVITY")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("ACTIVITY", (string)"NULL"));
                                break;
                            case 5:
                                if (source.ElementAt(i).Name == "SUB_ELEMENT")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("SUB_ELEMENT", (string)"NULL"));
                                break;
                            case 6:
                                if (source.ElementAt(i).Name == "APPN")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("APPN", (string)"NULL"));
                                break;
                            case 7:
                                if (source.ElementAt(i).Name == "APPN_NO")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("APPN_NO", (string)"NULL"));
                                break;
                            case 8:
                                if (source.ElementAt(i).Name == "DOC_TYPE")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("DOC_TYPE", (string)"NULL"));
                                break;
                            case 9:
                                if (source.ElementAt(i).Name == "MIS_PROGRAM")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("MIS_PROGRAM", (string)"NULL"));
                                break;
                            case 10:
                                if (source.ElementAt(i).Name == "MIS_PROJECT")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("MIS_PROJECT", (string)"NULL"));
                                break;
                            case 11:
                                if (source.ElementAt(i).Name == "TITLE")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("TITLE", (string)"NULL"));
                                break;
                            case 12:
                                if (source.ElementAt(i).Name == "SPONSOR")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("SPONSOR", (string)"NULL"));
                                break;
                            case 13:
                                if (source.ElementAt(i).Name == "ENGINEER")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("ENGINEER", (string)"NULL"));
                                break;
                            case 14:
                                if (source.ElementAt(i).Name == "LAB_ALLOC")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("LAB_ALLOC", (string)"0"));
                                break;
                            case 15:
                                if (source.ElementAt(i).Name == "MAT_ALLOC")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("MAT_ALLOC", (string)"0"));
                                break;
                            case 16:
                                if (source.ElementAt(i).Name == "TRV_ALLOC")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("TRV_ALLOC", (string)"0"));
                                break;
                            case 17:
                                if (source.ElementAt(i).Name == "SVC_ALLOC")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("SVC_ALLOC", (string)"0"));
                                break;
                            case 18:
                                if (source.ElementAt(i).Name == "DIV_ALLOC")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("DIV_ALLOC", (string)"0"));
                                break;
                            case 19:
                                if (source.ElementAt(i).Name == "CB_ALLOC")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("CB_ALLOC", (string)"0"));
                                break;
                            case 20:
                                if (source.ElementAt(i).Name == "OTHER_ALLOC")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("OTHER_ALLOC", (string)"0"));
                                break;
                            case 21:
                                if (source.ElementAt(i).Name == "TOTAL_ALLOCATED")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("TOTAL_ALLOCATED", (string)"0"));
                                break;
                            case 22:
                                if (source.ElementAt(i).Name == "LAB_EXP")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("LAB_EXP", (string)"0"));
                                break;
                            case 23:
                                if (source.ElementAt(i).Name == "MAT_EXP")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("MAT_EXP", (string)"0"));
                                break;
                            case 24:
                                if (source.ElementAt(i).Name == "TRV_EXP")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("TRV_EXP", (string)"0"));
                                break;
                            case 25:
                                if (source.ElementAt(i).Name == "SVC_EXP")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("SVC_EXP", (string)"0"));
                                break;
                            case 26:
                                if (source.ElementAt(i).Name == "DIV_EXP")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("DIV_EXP", (string)"0"));
                                break;
                            case 27:
                                if (source.ElementAt(i).Name == "CB_EXP")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("CB_EXP", (string)"0"));
                                break;
                            case 28:
                                if (source.ElementAt(i).Name == "OTHER_EXP")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("OTHER_EXP", (string)"0"));
                                break;
                            case 29:
                                if (source.ElementAt(i).Name == "TOTAL_EXPENDED")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("TOTAL_EXP", (string)"0"));
                                break;
                            case 30:
                                if (source.ElementAt(i).Name == "LAB_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("LAB_BAL", (string)"0"));
                                break;
                            case 31:
                                if (source.ElementAt(i).Name == "MAT_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("MAT_BAL", (string)"0"));
                                break;
                            case 32:
                                if (source.ElementAt(i).Name == "TRV_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("TRV_BAL", (string)"0"));
                                break;
                            case 33:
                                if (source.ElementAt(i).Name == "SVC_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("SVC_BAL", (string)"0"));
                                break;
                            case 34:
                                if (source.ElementAt(i).Name == "DIV_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("DIV_BAL", (string)"0"));
                                break;
                            case 35:
                                if (source.ElementAt(i).Name == "CB_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("CB_BAL", (string)"0"));
                                break;
                            case 36:
                                if (source.ElementAt(i).Name == "OTHER_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("OTHER_BAL", (string)"0"));
                                break;
                            case 37:
                                if (source.ElementAt(i).Name == "TOTAL_BAL")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("TOTAL_BAL", (string)"0"));
                                break;
                            case 38:
                                if (source.ElementAt(i).Name == "WCD")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("WCD", (string)"1/1/1900"));
                                break;
                            case 39:
                                if (source.ElementAt(i).Name == "APPN_EXP_DT")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("APPN_EXP_DT", (string)"1/1/1900"));
                                break;
                            case 40:
                                if (source.ElementAt(i).Name == "FUNDS_ACCEPT_DT")
                                {
                                    break;
                                }
                                source.Insert(i, new XAttribute("FUNDS_ACCEPT_DT", (string)"1/1/1900"));
                                break;
                        }
                    }
                    foreach (var item in source)
                    {
                        output.Add(item.Value);
                    }
                    break;
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
    }
}
