using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace ProjectManager.Model
{
    public class ResourceDictionaryCollection : ObservableCollection<ResourceDictionary>
    {
        public ResourceDictionaryCollection() { }
    }
}
