using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    [Serializable]
    public class TestingClass
    {
        public decimal Age { get; set; }
        public string Name { get; set; }
        public DateTime currentDate { get; set; }
        //public TestingClass() { }

        public TestingClass()
        {
            Age = (decimal)33.35;
            Name = "James";
            currentDate = DateTime.Today;
            
        }
    }
}
