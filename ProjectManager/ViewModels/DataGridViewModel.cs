using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace ProjectManager.ViewModels
{
    [Serializable]
    public class DataGridViewModel : ObservableCollection<DataGridViewModel>, INotifyPropertyChanged
    {
        public string MIPRnum { get; set; }       
        public string ProjNum { get; set; }      
        public string BillingElem { get; set; }      
        public string Network { get; set; }      
        public string Activity { get; set; }      
        public string SubElem { get; set; }      
        public string Appn { get; set; }      
        public string AppnNo { get; set; }      
        public string DocType { get; set; }      
        public string Program { get; set; }       
        public string Project { get; set; }      
        public string Title { get; set; }      
        public string Sponsor { get; set; }      
        public string Engineer { get; set; }      
        public string LabAlloc { get; set; }      
        public string MatAlloc { get; set; }      
        public string TrvAlloc { get; set; }      
        public string SvcAlloc { get; set; }      
        public string DivAlloc { get; set; }      
        public string CBAlloc { get; set; }      
        public string OtherAlloc { get; set; }      
        public string TotalAlloc { get; set; }      
        public string LabExp { get; set; }      
        public string MatExp { get; set; }      
        public string TrvExp { get; set; }      
        public string SvcExp { get; set; }      
        public string DivExp { get; set; }      
        public string CBExp { get; set; }      
        public string OtherExp { get; set; }      
        public string TotalExp { get; set; }      
        public string LabBal { get; set; }      
        public string MatBal { get; set; }      
        public string TrvBal { get; set; }      
        public string SvcBal { get; set; }      
        public string DivBal { get; set; }      
        public string CBBal { get; set; }      
        public string OtherBal { get; set; }      
        public string TotalBal { get; set; }      
        public string WCD { get; set; }      
        public string AppnExp { get; set; }      
        public string AcceptDate { get; set; }

#region Constructors

        public DataGridViewModel(string _MIPRnum, string _ProjNum, string _BillingElem, string _Network, string _Activity, string _SubElem, string _Appn, string _AppnNo, string _DocType, string _Program,
            string _Project, string _Title, string _Sponsor, string _Engineer, string _LabAlloc, string _MatAlloc, string _TrvAlloc, string _SvcAlloc, string _DivAlloc, string _CBAlloc,
            string _OtherAlloc, string _TotalAlloc, string _LabExp, string _MatExp, string _TrvExp, string _SvcExp, string _DivExp, string _CBExp, string _OtherExp, string _TotalExp,
            string _LabBal, string _MatBal, string _TrvBal, string _SvcBal, string _DivBal, string _CBBal, string _OtherBal, string _TotalBal, string _WCD, string _AppnExp, string _AcceptDate)
        {
            MIPRnum = _MIPRnum;
            ProjNum = _ProjNum;
            BillingElem = _BillingElem;
            Network = _Network;
            Activity = _Activity;
            SubElem = _SubElem;
            Appn = _Appn;
            AppnNo = _AppnNo;
            DocType = _DocType;
            Program = _Program;
            Project = _Project;
            Title = _Title;
            Sponsor = _Sponsor;
            Engineer = _Engineer;
            LabAlloc = _LabAlloc;
            MatAlloc = _MatAlloc;
            TrvAlloc = _TrvAlloc;
            SvcAlloc = _SvcAlloc;
            DivAlloc = _DivAlloc;
            CBAlloc = _CBAlloc;
            OtherAlloc = _OtherAlloc;
            TotalAlloc = _TotalAlloc;
            LabExp = _LabExp;
            MatExp = _MatExp;
            TrvExp = _TrvExp;
            SvcExp = _SvcExp;
            DivExp = _DivExp;
            CBExp = _CBExp;
            OtherExp = _OtherExp;
            TotalExp = _TotalExp;
            LabBal = _LabBal;
            MatBal = _MatBal;
            TrvBal = _TrvBal;
            SvcBal = _SvcBal;
            DivBal = _DivBal;
            CBBal = _CBBal;
            OtherBal = _OtherBal;
            TotalBal = _TotalBal;
            WCD = _WCD;
            AppnExp = _AppnExp;
            AcceptDate = _AcceptDate;
        }

        public DataGridViewModel(string _MIPRnum, string _ProjNum, ProjectNumberViewModel number)
        {
            MIPRnum = _MIPRnum;
            ProjNum = _ProjNum;
            BillingElem = number.Children[0];
            Network = number.Children[1];
            Activity = number.Children[2];
            SubElem = number.Children[3];
            Appn = number.Children[4];
            AppnNo = number.Children[5];
            DocType = number.Children[6];
            Program = number.Children[7];
            Project = number.Children[8];
            Title = number.Children[9];
            Sponsor = number.Children[10];
            Engineer = number.Children[11];
            LabAlloc = number.Children[12];
            MatAlloc = number.Children[13];
            TrvAlloc = number.Children[14];
            SvcAlloc = number.Children[15];
            DivAlloc = number.Children[16];
            CBAlloc = number.Children[17];
            OtherAlloc = number.Children[18];
            TotalAlloc = number.Children[19];
            LabExp = number.Children[20];
            MatExp = number.Children[21];
            TrvExp = number.Children[22];
            SvcExp = number.Children[23];
            DivExp = number.Children[24];
            CBExp = number.Children[25];
            OtherExp = number.Children[26];
            TotalExp = number.Children[27];
            LabBal = number.Children[28];
            MatBal = number.Children[29];
            TrvBal = number.Children[30];
            SvcBal = number.Children[31];
            DivBal = number.Children[32];
            CBBal = number.Children[33];
            OtherBal = number.Children[34];
            TotalBal = number.Children[35];
            WCD = number.Children[36];
            AppnExp = number.Children[37];
            AcceptDate = number.Children[38];
        }

        public DataGridViewModel() { }

        #endregion

#region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

#region Methods

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private static decimal Parse(string input)
        {
            string pattern = @"[\$\,]";
            return decimal.Parse(Regex.Replace(input, pattern, ""));
        }

        public static ChartDataViewModel GetTotals(ObservableCollection<DataGridViewModel> input)
        {
            decimal[] totals = new decimal[24];

            foreach (var item in input)
            {
                decimal x = Parse(item.LabAlloc);
                totals[0] += x;
                x = Parse(item.MatAlloc);
                totals[1] += x;
                x = Parse(item.TrvAlloc);
                totals[2] += x;
                x = Parse(item.SvcAlloc);
                totals[3] += x;
                x = Parse(item.DivAlloc);
                totals[4] += x;
                x = Parse(item.CBAlloc);
                totals[5] += x;
                x = Parse(item.OtherAlloc);
                totals[6] += x;
                x = Parse(item.TotalAlloc);
                totals[7] += x;
                x = Parse(item.LabExp);
                totals[8] += x;
                x = Parse(item.MatExp);
                totals[9] += x;
                x = Parse(item.TrvExp);
                totals[10] += x;
                x = Parse(item.SvcExp);
                totals[11] += x;
                x = Parse(item.DivExp);
                totals[12] += x;
                x = Parse(item.CBExp);
                totals[13] += x;
                x = Parse(item.OtherExp);
                totals[14] += x;
                x = Parse(item.TotalExp);
                totals[15] += x;
                x = Parse(item.LabBal);
                totals[16] += x;
                x = Parse(item.MatBal);
                totals[17] += x;
                x = Parse(item.TrvBal);
                totals[18] += x;
                x = Parse(item.SvcBal);
                totals[19] += x;
                x = Parse(item.DivBal);
                totals[20] += x;
                x = Parse(item.CBBal);
                totals[21] += x;
                x = Parse(item.OtherBal);
                totals[22] += x;
                x = Parse(item.TotalBal);
                totals[23] += x;
            }

            string[] stringTotals = new string[24];
            for (int i = 0; i < stringTotals.Count(); i++)
            {
                stringTotals[i] = string.Format("{0:C}", totals[i]);
            }

            DataGridViewModel totalRow = new DataGridViewModel(null, null, null, null, null, null, null, null, null, null, null, null, null, "TOTALS:", stringTotals[0],
                stringTotals[1], stringTotals[2], stringTotals[3], stringTotals[4], stringTotals[5], stringTotals[6], stringTotals[7], stringTotals[8], stringTotals[9], stringTotals[10],
                stringTotals[11], stringTotals[12], stringTotals[13], stringTotals[14], stringTotals[15], stringTotals[16], stringTotals[17], stringTotals[18], stringTotals[19],
                stringTotals[20], stringTotals[21], stringTotals[22], stringTotals[23], null, null, null);
            
            input.Add(totalRow);

            return new ChartDataViewModel(Parse(totalRow.TotalAlloc), Parse(totalRow.TotalExp), Parse(totalRow.TotalBal), DateTime.Now);
        }

        #endregion Methods
    }
}
