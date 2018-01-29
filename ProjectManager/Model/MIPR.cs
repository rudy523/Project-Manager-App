using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ProjectManager;
using System.Text.RegularExpressions;


namespace ProjectManager.Model
{
    [Serializable]
    public class MIPR 
    {
#region Properties
        public string MIPRnum { get; set; }
        public ObservableCollection<ProjectNumber> MIPRdetails { get; set; }
        public List<ProjNum> ProjectNums { get; set; }
        #endregion

        #region Constructors
        // Empty constructor for XML Serializer (save function)
        public MIPR() { }

        public MIPR(string miprNum, ObservableCollection<ProjectNumber> miprDetails)
        {
            MIPRnum = miprNum;
            MIPRdetails = miprDetails;
        }

        public MIPR(string miprNum, List<ProjNum> miprDetails)
        {
            MIPRnum = miprNum;
            ProjectNums = miprDetails;
        }

        public MIPR(string miprNum)
        {
            MIPRnum = miprNum;
        }

        #endregion
    }
}
