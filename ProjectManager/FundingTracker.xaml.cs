using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ProjectManager.ViewModels;
using Microsoft.Win32;
using ProjectManager.Model;
using System.IO;


namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for FundingTracker.xaml
    /// </summary>
    [Serializable]
    public partial class FundingTracker : Window
    {
        public string[] SearchCriteria { get; set; }
        public MainViewModel _viewModel { get; set; }

        public FundingTracker()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {         
                SaveLoadState.Save("SaveData.xml", _viewModel);               
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.RestoreDirectory = true;
            saveDlg.ShowDialog();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {           
                _viewModel = SaveLoadState.Load("SaveData.xml");
                _viewModel.ReinstateParentObjects();
                DataContext = _viewModel;
                FundingGrid.Visibility = Visibility.Visible;
                MIPRdisplay.Visibility = Visibility.Visible;
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
   
            this.SearchCriteria = SearchBox.Text.Split(' ');
            _viewModel.SearchData(SearchCriteria);
            base.DataContext = _viewModel;
          
            //MIPRdisplay.ItemsSource = _viewModel.MIPRnums;
            MIPRdisplay.Visibility = Visibility.Visible;         
        }

        private void ProjectNumberSelected(object sender, MouseButtonEventArgs e)
        {
            ProjectNumPopup.IsOpen = true;
        }

        /*
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                DataGridScroller.LineDown();
            }
            else
            {
                DataGridScroller.LineUp();
            }
            e.Handled = true;
        }
        */

        private void TrackFunding_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TrackFunding();
            if (base.DataContext != null)
            {
                FundingGrid.Visibility = Visibility.Visible;
            }
            
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //base.DataContext = null;
            _viewModel.MIPRnums.Clear();
            _viewModel.GridData.Clear();
            _viewModel.ChartDataModel.Clear();
            _viewModel.MIPRsummary.Clear();
            SearchBox.Clear();
            FundingGrid.Visibility = Visibility.Hidden;
            MIPRdisplay.Visibility = Visibility.Hidden;
        }

        private void ShowTree_Click(object sender, RoutedEventArgs e)
        {
            VisualTreeDisplay treeDisplay = new VisualTreeDisplay();
            treeDisplay.ShowVisualTree(this);
            treeDisplay.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateFunding();
        }

        private void ProjNum_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {

        }
    }
}
