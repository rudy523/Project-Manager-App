using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.ViewModels;

namespace ProjectManager.Model
{
    public class TDLcomparer : EqualityComparer<TDLViewModel>
    {
        public override bool Equals(TDLViewModel t1, TDLViewModel t2)
        {
            if (t1 == null && t2 == null)
            {
                return true;
            }
            else if (t1 == null || t2 == null)
            {
                return false;
            }
            if (t1.TDL_No == t2.TDL_No)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode(TDLViewModel tdl)
        {
            int hcode = (int)tdl.BudgTotal;
            return hcode.GetHashCode();
        }
    }
}
