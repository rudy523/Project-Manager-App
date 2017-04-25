using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using System.ComponentModel;

namespace ProjectManager.ViewModels
{
    public class MIPRViewModel : INotifyPropertyChanged
    {
        // Properties
        readonly IReadOnlyCollection<ProjectNumberViewModel> _MIPRchildren;
        public IReadOnlyCollection<ProjectNumberViewModel> MIPRchildren
        {
            get { return _MIPRchildren; }
        }
        readonly MIPR _MIPR;
        public MIPR MIPR
        {
            get { return _MIPR; }
        }
        public string MIPRName
        {
            get { return MIPR.MIPRnum; }
        }
        bool _isExpanded;
        bool _isSelected;

        // Constructor
        public MIPRViewModel(MIPR mipr)
        {
            List<ProjectNumberViewModel> nums = new List<ProjectNumberViewModel>();
            foreach (var item in mipr.MIPRdetails)
            {
                ProjectNumberViewModel num = new ProjectNumberViewModel(item, MIPR);
                nums.Add(num);
            }
            _MIPRchildren = nums;
            _MIPR = mipr;
        }

        // Methods
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
