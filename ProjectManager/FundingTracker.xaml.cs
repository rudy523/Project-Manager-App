using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ProjectManager.ViewModels;
using Microsoft.Win32;
using ProjectManager.Model;
using System.Collections.Generic;
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
            base.DataContext = _viewModel;
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
            //base.DataContext = _viewModel;
          
            //MIPRdisplay.ItemsSource = _viewModel.MIPRnums;
            MIPRdisplay.Visibility = Visibility.Visible;         
        }

        private void ProjectNumberSelected(object sender, MouseButtonEventArgs e)
        {
            ProjectNumPopup.IsOpen = true;
        }

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

        private void Filter_Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedEngineers = new List<string>();
            List<string> selectedContracts = new List<string>();
            bool useCurrent;
            DateTime selectedStart;
            DateTime selectedEnd;

            _viewModel.filteredTDLs.Clear();

            foreach (var item in EngList.SelectedItems)
            {
                selectedEngineers.Add(item.ToString());
            }

            foreach (var item in ContractList.SelectedItems)
            {
                selectedContracts.Add(item.ToString());
            }

            if (CurrentCheck.IsChecked == true)
            {
                useCurrent = true;
            }
            else
            {
                useCurrent = false;
            }

            if (StartDatePick.SelectedDate != null)
            {
                selectedStart = (DateTime)StartDatePick.SelectedDate;
            }
            
            else
            {
                selectedStart = DateTime.Parse("1/1/1900");
            }
         
            if (EndDatePick.SelectedDate != null)
            {
                selectedEnd = (DateTime)EndDatePick.SelectedDate;
            }
           
            else
            {
                selectedEnd = DateTime.Parse("1/1/1900");
            }
         
            _viewModel.myTDL.RunQuery(selectedEngineers, selectedContracts, useCurrent, selectedStart, selectedEnd);

            foreach (var item in _viewModel.myTDL.FilteredTDLs)
            {
                _viewModel.filteredTDLs.Add(item);
            }        
        }

        private void CurrentCheck_Click(object sender, RoutedEventArgs e)
        {
            StartDatePick.IsEnabled = false;
            EndDatePick.IsEnabled = false;
            CustomDateCheck.IsChecked = false;
        }

        private void CustomDateCheck_Click(object sender, RoutedEventArgs e)
        {
            StartDatePick.IsEnabled = true;
            EndDatePick.IsEnabled = true;
            CurrentCheck.IsChecked = false;
        }

        private void Reset_Param_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
