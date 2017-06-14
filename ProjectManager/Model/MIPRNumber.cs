using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager
{
    [Serializable]
    public class MIPRNumber
    {
        public string MIPRnum { get; set; }
        public string Projnum { get; set; }
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

        public bool IsSelected { get; set; }

        public MIPRNumber(string _MIPRnum, string _Projnum, string _BillingElem, string _Network, string _Activity, string _SubElem, string _Appn, string _AppnNo, string _DocType, string _Program, string _Project, 
            string _Title, string _Sponsor, string _Engineer, decimal _LabAlloc, decimal _MatAlloc, decimal _TrvAlloc, decimal _SvcAlloc, decimal _DivAlloc, decimal _CBAlloc, decimal _OtherAlloc,
            decimal _TotalAlloc, decimal _LabExp, decimal _MatExp, decimal _TrvExp, decimal _SvcExp, decimal _DivExp, decimal _CBExp, decimal _OtherExp, decimal _TotalExp, decimal _LabBal, decimal _MatBal,
            decimal _TrvBal, decimal _SvcBal, decimal _DivBal, decimal _CBBal, decimal _OtherBal, decimal _TotalBal, DateTime _WCD, DateTime _AppnExp, DateTime _AcceptDate)
        {
            MIPRnum = _MIPRnum;
            Projnum = _Projnum;
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

    }

    
}
