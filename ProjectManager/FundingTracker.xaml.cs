using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ProjectManager.Model;
using System.ComponentModel;
using ProjectManager.ViewModels;

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for FundingTracker.xaml
    /// </summary>
    public partial class FundingTracker : Window
    {
        public string[] SearchCriteria { get; set; }
        //public ObservableCollection<MIPR> FilteredData { get; set; }
        //public ObservableCollection<MIPRNumber> SearchData { get; set; }
        public bool MIPRchecked { get; set; }
        public MainViewModel _viewModel { get; set; }

        public FundingTracker()
        {
            InitializeComponent();

            //Command Bindings
            CommandBinding newBinding = new CommandBinding(ApplicationCommands.New);
            newBinding.Executed += NewCommand_Executed;
            this.CommandBindings.Add(newBinding);

        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FundingTracker newFund = new FundingTracker();
            newFund.Show();
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            this.SearchCriteria = SearchBox.Text.Split(' ');
            _viewModel = new MainViewModel(SearchCriteria);
            //base.DataContext = _viewModel;

            //MIPRdisplay.ItemsSource = FilteredData;
            MIPRdisplay.Visibility = Visibility.Visible;
            
        }

        private void MIPRdisplay_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DetailsList.Visibility = Visibility.Visible;
        }

        private void MIPRcheck_Checked(object sender, RoutedEventArgs e)
        {
           
  
        }

       
    }
}
