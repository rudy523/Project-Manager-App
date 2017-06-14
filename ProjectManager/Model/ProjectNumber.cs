using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProjectManager.Model
{
    [Serializable]
    public class ProjectNumber 
    {   
        public string Projnum { get; set; }
        public ObservableCollection<string> Details { get; set; }  
        public ProjectNumber() { }
        public ProjectNumber(string projnum, ObservableCollection<string> details)
        {
            Projnum = projnum;
            Details = details;
        }

    }
}
