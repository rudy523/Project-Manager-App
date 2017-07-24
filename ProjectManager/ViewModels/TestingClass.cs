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
        public List<int> myNumbers;

        public TestingClass()
        {
            myNumbers = new List<int>();
            for (int i = 0; i < 101; i++)
            {
                myNumbers.Add(i);
            }
        }
    }
}
