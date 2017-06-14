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
    public class ProjectNumberViewModel : INotifyPropertyChanged
    {
        #region Properties
        [XmlElement]
        public string ProjNum { get; set; }
        public ObservableCollection<string> Children { get; set; }
        public List<bool> SiblingChecks { get; set; }
        public string ParentNumber { get; set; }
        public bool _isExpanded { get; set; }
        public bool _isSelected { get; set; }
        public bool _isChecked { get; set; }
        public bool IsInitiallySelected { get; set; }
        public int Counter { get; set; }
        [XmlIgnore]
        public MIPRViewModel Parent { get; set; }
        #endregion

        #region Constructor
        public ProjectNumberViewModel() { }
        public ProjectNumberViewModel(ProjectNumber projectnumber, MIPRViewModel parent)
        {
            Parent = parent;
            Children = projectnumber.Details;
            ParentNumber = parent.MIPRName;
            ProjNum = projectnumber.Projnum;
        }
#endregion

        #region Methods
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;                
                }
                if (_isExpanded && Parent != null)
                {
                    Parent.IsExpanded = true;
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
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        public bool IsChecked
        {          
            get { return _isChecked; }
            set
            {
                if (value != _isChecked)
                {
                    Counter = 0;
                    _isChecked = value;
                    foreach (var item in Parent.MIPRchildren)
                    {
                        if (item.IsChecked != true)
                        {
                            Counter++;
                        }                    
                    }
                    if (Counter == 0)
                    {
                        Parent.ParentUpdate = true;
                        Parent.IsChecked = value;
                    }                
                    if (value == false)
                    {
                        Parent.ParentUpdate = true;
                        Parent.IsChecked = value;                      
                    }
                    if (Counter == Parent.MIPRchildren.Count())
                    {
                        Parent.IsExpanded = false;
                    }
                    if (PropertyChanged != null)
                    {
                        this.OnPropertyChanged("IsChecked");
                    }            
                }                
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
#endregion
}
