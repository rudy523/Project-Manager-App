using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;



namespace ProjectManager.ViewModels
{
    [Serializable]
    public class MIPRViewModel : INotifyPropertyChanged
    {
        // Properties
        [XmlElement]
        public string MIPRName { get; set; }
        public List<ProjectNumberViewModel> MIPRchildren { get; set; }
        public bool _isExpanded { get; set; }
        public bool _isSelected { get; set; }
        public bool _isChecked { get; set; }
        public bool ParentUpdate { get; set; }

        // Constructors
        public MIPRViewModel() { }
        public MIPRViewModel(MIPR mipr)
        {
            MIPRName = mipr.MIPRnum;
            List<ProjectNumberViewModel> nums = new List<ProjectNumberViewModel>();
            foreach (var item in mipr.MIPRdetails)
            {
                ProjectNumberViewModel num = new ProjectNumberViewModel(item, this);
                nums.Add(num);
            }
            MIPRchildren = nums;
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    if (PropertyChanged != null)
                    {
                        this.OnPropertyChanged("IsExpanded");
                    }                 
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
                    if (PropertyChanged != null)
                    {
                        this.OnPropertyChanged("IsExpanded");
                    }
                }
            }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                IsExpanded = true;
                if (value != _isChecked)
                {
                    if (ParentUpdate == true)
                    {
                        _isChecked = value;
                        this.OnPropertyChanged("IsChecked");
                    }
                    else if (ParentUpdate == false)
                    {
                        _isChecked = value;
                        foreach (var item in MIPRchildren)
                        {
                            item.IsChecked = value;
                        }
                        if (value == false)
                        {
                            IsExpanded = false;
                        }
                    }      
                }
                ParentUpdate = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
