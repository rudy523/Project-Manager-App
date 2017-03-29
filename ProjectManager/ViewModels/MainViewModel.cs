using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManager.Model;
using System.Collections.ObjectModel;

namespace ProjectManager
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public LoadData MyData { get; set; }
        public ObservableCollection<ProjectNumber> ProjectNumbers { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            this.MyData = new LoadData();
            this.ProjectNumbers = MyData.ProjectNumbersView;
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
