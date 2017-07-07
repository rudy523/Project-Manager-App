using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.ViewModels;

namespace ProjectManager.Model
{
    class DataGridComparer : IEqualityComparer<DataGridViewModel>
    {
        // Data Grid objects are equal if their project numbers are equal
        public bool Equals(DataGridViewModel x, DataGridViewModel y)
        {
            // Check whether the compared objects reference the same data
            if (object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) return false;
            return x.ProjNum == y.ProjNum;       
        }

        // If Equals() return true for a pair of objects, then GetHashCode() must return the same value for these objects.
        public int GetHashCode(DataGridViewModel gridobject)
        {
            // Check whether the object is null
            if (object.ReferenceEquals(gridobject, null)) return 0;

            // Get hash code for the Project Number field
            int hashProjNum = gridobject.ProjNum.GetHashCode();

            return hashProjNum;
        }
    }
}
