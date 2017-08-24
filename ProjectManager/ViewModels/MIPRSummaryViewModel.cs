using System;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ProjectManager.ViewModels
{
    [Serializable]
    public class MIPRSummaryViewModel 
    {
        #region Properties
        public string MIPRnum { get; set; }
        public string BillingElem { get; set; }
        public string Appn { get; set; }
        public string AppnNo { get; set; }
        public string Program { get; set; }
        public string Sponsor { get; set; }
        public decimal LabAlloc { get; set; }
        public decimal MatAlloc { get; set; }
        public decimal TrvAlloc { get; set; }
        public decimal SvcAlloc { get; set; }
        public decimal DivAlloc { get; set; }
        public decimal CBAlloc { get; set; }
        public decimal OtherAlloc { get; set; }
        public decimal TotalAlloc { get; set; }
        public decimal LabExp { get; set; }
        public decimal MatExp { get; set; }
        public decimal TrvExp { get; set; }
        public decimal SvcExp { get; set; }
        public decimal DivExp { get; set; }
        public decimal CBExp { get; set; }
        public decimal OtherExp { get; set; }
        public decimal TotalExp { get; set; }
        public decimal LabBal { get; set; }
        public decimal MatBal { get; set; }
        public decimal TrvBal { get; set; }
        public decimal SvcBal { get; set; }
        public decimal DivBal { get; set; }
        public decimal CBBal { get; set; }
        public decimal OtherBal { get; set; }
        public decimal TotalBal { get; set; }
        public DateTime WCD { get; set; }
        public DateTime AppnExp { get; set; }
        public DateTime AcceptDate { get; set; }
        public decimal[] MIPRtotals { get; set; }
        public decimal TotalDC { get; set; }
        public decimal TotalWR { get; set; }
        public decimal TotalPO { get; set; }
        public decimal PercentDC { get; set; }
        public decimal PercentWR { get; set; }
        public decimal PercentPO { get; set; }
        public decimal PercentExp { get; set; }
        public decimal PercentRem { get; set; }
        #endregion

        #region Constructors

        // Empty constructor for XML Serializer (save function)
        public MIPRSummaryViewModel() { }

            public MIPRSummaryViewModel(string name, IEnumerable<DataGridViewModel> input)
            {
            MIPRnum = name;
            MIPRtotals = new decimal[24];
            foreach (var item in input)
            {
                MIPRtotals[0] += Parse(item.LabAlloc);
                MIPRtotals[1] += Parse(item.MatAlloc);
                MIPRtotals[2] += Parse(item.TrvAlloc);
                MIPRtotals[3] += Parse(item.SvcAlloc);
                MIPRtotals[4] += Parse(item.DivAlloc);
                MIPRtotals[5] += Parse(item.CBAlloc);
                MIPRtotals[6] += Parse(item.OtherAlloc);
                MIPRtotals[7] += Parse(item.TotalAlloc);
                MIPRtotals[8] += Parse(item.LabExp);
                MIPRtotals[9] += Parse(item.MatExp);
                MIPRtotals[10] += Parse(item.TrvExp);
                MIPRtotals[11] += Parse(item.SvcExp);
                MIPRtotals[12] += Parse(item.DivExp);
                MIPRtotals[13] += Parse(item.CBExp);
                MIPRtotals[14] += Parse(item.OtherExp);
                MIPRtotals[15] += Parse(item.TotalExp);
                MIPRtotals[16] += Parse(item.LabBal);
                MIPRtotals[17] += Parse(item.MatBal);
                MIPRtotals[18] += Parse(item.TrvBal);
                MIPRtotals[19] += Parse(item.SvcBal);
                MIPRtotals[20] += Parse(item.DivBal);
                MIPRtotals[21] += Parse(item.CBBal);
                MIPRtotals[22] += Parse(item.OtherBal);
                MIPRtotals[23] += Parse(item.TotalBal);
                GetFundingTypes(item);
            }
            LabAlloc = MIPRtotals[0];
            MatAlloc = MIPRtotals[1];
            TrvAlloc = MIPRtotals[2];
            SvcAlloc = MIPRtotals[3];
            DivAlloc = MIPRtotals[4];
            CBAlloc = MIPRtotals[5];
            OtherAlloc = MIPRtotals[6];
            TotalAlloc = MIPRtotals[7];
            LabExp = MIPRtotals[8];
            MatExp = MIPRtotals[9];
            TrvExp = MIPRtotals[10];
            SvcExp = MIPRtotals[11];
            DivExp = MIPRtotals[12];
            CBExp = MIPRtotals[13];
            OtherExp = MIPRtotals[14];
            TotalExp = MIPRtotals[15];
            LabBal = MIPRtotals[16];
            MatBal = MIPRtotals[17];
            TrvBal = MIPRtotals[18];
            SvcBal = MIPRtotals[19];
            DivBal = MIPRtotals[20];
            CBBal = MIPRtotals[21];
            OtherBal = MIPRtotals[22];
            TotalBal = MIPRtotals[23];
            BillingElem = input.ElementAt(0).BillingElem;
            Appn = input.ElementAt(0).Appn;
            AppnNo = input.ElementAt(0).AppnNo;
            Program = input.ElementAt(0).Program;
            Sponsor = input.ElementAt(0).Sponsor;
            WCD = DateTime.Parse(input.ElementAt(0).WCD);
            AppnExp = DateTime.Parse(input.ElementAt(0).AppnExp);
            AcceptDate = DateTime.Parse(input.ElementAt(0).AcceptDate);
            PercentDC = TotalDC / TotalAlloc;
            PercentWR = TotalWR / TotalAlloc;
            PercentPO = TotalPO / TotalAlloc;
            PercentExp = TotalExp / TotalAlloc;
            PercentRem = TotalBal / TotalAlloc;


        }

            #endregion

        #region Methods

            private static decimal Parse(string input)
            {
                string pattern = @"[\$\,]";
                return decimal.Parse(Regex.Replace(input, pattern, ""));
            }

            private void GetFundingTypes(DataGridViewModel input)
        {
            switch (input.DocType)
            {
                case "WR":
                    TotalWR += Parse(input.TotalAlloc);
                    break;
                case "RC":
                    TotalDC += Parse(input.TotalAlloc);
                    break;
                case "PO":
                    TotalPO += Parse(input.TotalAlloc);
                    break;
                default:
                    break;
            }
        }
       


        #endregion
    }
}
