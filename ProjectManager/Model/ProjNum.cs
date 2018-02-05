using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectManager.Model
{
    public class ProjNum 
    {
        public string MIPRnum { get; set; }
        public string Projnum { get; set; }
        public string BillingElem { get; set; }
        public string Network { get; set; }
        public string Activity { get; set; }
        public string ActElem { get; set; }
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

        public ProjNum(IEnumerable<XAttribute> source)
        {
            try
            {
                if (source.Count() == 41)
                {
                    MIPRnum = source.ElementAt(0).Value.ToString();
                    Projnum = source.ElementAt(1).Value.ToString();
                    BillingElem = source.ElementAt(2).Value.ToString();
                    Network = source.ElementAt(3).Value.ToString();
                    Activity = source.ElementAt(4).Value.ToString();
                    ActElem = source.ElementAt(5).Value.ToString();
                    Appn = source.ElementAt(6).Value.ToString();
                    AppnNo = source.ElementAt(7).Value.ToString();
                    DocType = source.ElementAt(8).Value.ToString();
                    Program = source.ElementAt(9).Value.ToString();
                    Project = source.ElementAt(10).Value.ToString();
                    Title = source.ElementAt(11).Value.ToString();
                    Sponsor = source.ElementAt(12).Value.ToString();
                    Engineer = source.ElementAt(13).Value.ToString();
                    LabAlloc = decimal.Parse(source.ElementAt(14).Value.ToString());
                    MatAlloc = decimal.Parse(source.ElementAt(15).Value.ToString());
                    TrvAlloc = decimal.Parse(source.ElementAt(16).Value.ToString());
                    SvcAlloc = decimal.Parse(source.ElementAt(17).Value.ToString());
                    DivAlloc = decimal.Parse(source.ElementAt(18).Value.ToString());
                    CBAlloc = decimal.Parse(source.ElementAt(19).Value.ToString());
                    OtherAlloc = decimal.Parse(source.ElementAt(20).Value.ToString());
                    TotalAlloc = decimal.Parse(source.ElementAt(21).Value.ToString());
                    LabExp = decimal.Parse(source.ElementAt(22).Value.ToString());
                    MatExp = decimal.Parse(source.ElementAt(23).Value.ToString());
                    TrvExp = decimal.Parse(source.ElementAt(24).Value.ToString());
                    SvcExp = decimal.Parse(source.ElementAt(25).Value.ToString());
                    DivExp = decimal.Parse(source.ElementAt(26).Value.ToString());
                    CBExp = decimal.Parse(source.ElementAt(27).Value.ToString());
                    OtherExp = decimal.Parse(source.ElementAt(28).Value.ToString());
                    TotalExp = decimal.Parse(source.ElementAt(29).Value.ToString());
                    LabBal = decimal.Parse(source.ElementAt(30).Value.ToString());
                    MatBal = decimal.Parse(source.ElementAt(31).Value.ToString());
                    TrvBal = decimal.Parse(source.ElementAt(32).Value.ToString());
                    SvcBal = decimal.Parse(source.ElementAt(33).Value.ToString());
                    DivBal = decimal.Parse(source.ElementAt(34).Value.ToString());
                    CBBal = decimal.Parse(source.ElementAt(35).Value.ToString());
                    OtherBal = decimal.Parse(source.ElementAt(36).Value.ToString());
                    TotalBal = decimal.Parse(source.ElementAt(37).Value.ToString());
                    WCD = DateTime.Parse(source.ElementAt(38).Value.ToString());
                    AppnExp = DateTime.Parse(source.ElementAt(39).Value.ToString());
                    AcceptDate = DateTime.Parse(source.ElementAt(40).Value.ToString());
                }
                else if (source.Count() == 40)
                {
                    MIPRnum = source.ElementAt(0).Value.ToString();
                    Projnum = source.ElementAt(1).Value.ToString();
                    BillingElem = source.ElementAt(2).Value.ToString();
                    Network = source.ElementAt(3).Value.ToString();
                    Activity = source.ElementAt(4).Value.ToString();
                    ActElem = "NONE";
                    Appn = source.ElementAt(5).Value.ToString();
                    AppnNo = source.ElementAt(6).Value.ToString();
                    DocType = source.ElementAt(7).Value.ToString();
                    Program = source.ElementAt(8).Value.ToString();
                    Project = source.ElementAt(9).Value.ToString();
                    Title = source.ElementAt(10).Value.ToString();
                    Sponsor = source.ElementAt(11).Value.ToString();
                    Engineer = source.ElementAt(12).Value.ToString();
                    LabAlloc = decimal.Parse(source.ElementAt(13).Value.ToString());
                    MatAlloc = decimal.Parse(source.ElementAt(14).Value.ToString());
                    TrvAlloc = decimal.Parse(source.ElementAt(15).Value.ToString());
                    SvcAlloc = decimal.Parse(source.ElementAt(16).Value.ToString());
                    DivAlloc = decimal.Parse(source.ElementAt(17).Value.ToString());
                    CBAlloc = decimal.Parse(source.ElementAt(18).Value.ToString());
                    OtherAlloc = decimal.Parse(source.ElementAt(19).Value.ToString());
                    TotalAlloc = decimal.Parse(source.ElementAt(20).Value.ToString());
                    LabExp = decimal.Parse(source.ElementAt(21).Value.ToString());
                    MatExp = decimal.Parse(source.ElementAt(22).Value.ToString());
                    TrvExp = decimal.Parse(source.ElementAt(23).Value.ToString());
                    SvcExp = decimal.Parse(source.ElementAt(24).Value.ToString());
                    DivExp = decimal.Parse(source.ElementAt(25).Value.ToString());
                    CBExp = decimal.Parse(source.ElementAt(26).Value.ToString());
                    OtherExp = decimal.Parse(source.ElementAt(27).Value.ToString());
                    TotalExp = decimal.Parse(source.ElementAt(28).Value.ToString());
                    LabBal = decimal.Parse(source.ElementAt(29).Value.ToString());
                    MatBal = decimal.Parse(source.ElementAt(30).Value.ToString());
                    TrvBal = decimal.Parse(source.ElementAt(31).Value.ToString());
                    SvcBal = decimal.Parse(source.ElementAt(32).Value.ToString());
                    DivBal = decimal.Parse(source.ElementAt(33).Value.ToString());
                    CBBal = decimal.Parse(source.ElementAt(34).Value.ToString());
                    OtherBal = decimal.Parse(source.ElementAt(35).Value.ToString());
                    TotalBal = decimal.Parse(source.ElementAt(36).Value.ToString());
                    WCD = DateTime.Parse(source.ElementAt(37).Value.ToString());
                    AppnExp = DateTime.Parse(source.ElementAt(38).Value.ToString());
                    AcceptDate = DateTime.Parse(source.ElementAt(39).Value.ToString());
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("Project Number {0} is missing data, and will not be included. Verify MIS entry includes all fields.",source.ElementAt(1).ToString()));
                }
            }
            catch (Exception e)
            {
                
            }
           
            
        }
    }
}
