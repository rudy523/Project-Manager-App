using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManager.Model;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace ProjectManager.ViewModels
{
    public class MainViewModel 
    {
        public LoadData MyData { get; set; }
        public ObservableCollection<MIPR> ProjectNumbers { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        readonly IReadOnlyCollection<MIPRViewModel> MIPRnums;


        public MainViewModel(string[] input)
        {
            this.MyData = new LoadData(input);
            this.ProjectNumbers = this.MyData.MIPRcollection;
            List<MIPRViewModel> nums = new List<MIPRViewModel>();
            foreach (var item in this.ProjectNumbers)
            {
                MIPRViewModel num = new MIPRViewModel(item);
                nums.Add(num);
            }
            this.MIPRnums = nums;

        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
