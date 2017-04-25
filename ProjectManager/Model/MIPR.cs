using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ProjectManager.Model
{
    public class MIPR 
    {
        public string MIPRnum { get; set; }
        public ObservableCollection<ProjectNumber> MIPRdetails { get; set; }

        public MIPR(string miprNum, ObservableCollection<ProjectNumber> miprDetails)
        {
            MIPRnum = miprNum;
            MIPRdetails = miprDetails;
        }
    }
}
