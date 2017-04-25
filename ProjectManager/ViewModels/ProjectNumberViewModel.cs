using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using System.ComponentModel;

namespace ProjectManager.ViewModels
{
    public class ProjectNumberViewModel : INotifyPropertyChanged
    {
        // Properties
        readonly IReadOnlyCollection<string> _children;
        public IReadOnlyCollection<string> Children
        {
            get { return _children; }
        }
        readonly MIPR _Parent;
        public MIPR Parent
        {
            get { return _Parent; }
        }
        readonly ProjectNumber _projectnumber;
        public string ParentName
        {
            get { return Parent.MIPRnum; }
        }
        bool _isExpanded;
        bool _isSelected;

        // Constructor
        public ProjectNumberViewModel(ProjectNumber projectnumber, MIPR Parent)
        {
            _projectnumber = projectnumber;
            _Parent = Parent;
            _children = _projectnumber.Details;
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
