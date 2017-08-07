using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ProjectManager.ViewModels
{
    public class DataGridViewObjects
    {
        public ObservableCollection<DataGridViewModel> GridObjects { get; set; }

        public DataGridViewObjects(ObservableCollection<DataGridViewModel> input)
        {
            GridObjects = input;
        }
    }
}
