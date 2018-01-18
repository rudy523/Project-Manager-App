using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ProjectManager.ViewModels;
using Microsoft.Win32;
using ProjectManager.Model;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
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
            _viewModel.filteredTDLs.Clear();
            CurrentCheck.IsChecked = false;
            CustomDateCheck.IsChecked = false;
            StartDatePick.IsEnabled = false;
            EndDatePick.IsEnabled = false;
            EngList.SelectedItems.Clear();
            ContractList.SelectedItems.Clear();
        }

        private void CustomDateCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            StartDatePick.IsEnabled = false;
            EndDatePick.IsEnabled = false;
        }

        private void Track_Tasks_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in TaskList.SelectedItems)
            {
                int counter = 0;
                TDLViewModel temp = (TDLViewModel)item;
                foreach (var tdl in _viewModel.TrackedTDLs)
                {
                    if (tdl.TDL_No == temp.TDL_No)
                    {
                        counter++;
                    }
                }
                if (counter > 0)
                {
                    continue;
                }
                else
                {
                    _viewModel.TrackedTDLs.Add((TDLViewModel)item);
                }
            }
            TDLGrid.Visibility = Visibility.Visible;
        }

        private void Clear_Tasks_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TrackedTDLs.Clear();
            TDLGrid.Visibility = Visibility.Hidden;
        }

        private void CategoryPick_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_viewModel.SelectedTDL != null)
            {
                _viewModel.Budget.Clear();
                _viewModel.Funded.Clear();
                _viewModel.Expended.Clear();
                _viewModel.FundBalance.Clear();
                _viewModel.BudgetBalance.Clear();

                switch (_viewModel.SelectedCat)
                {
                    case "Labor":
                        _viewModel.Budget.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedTDL.BudgLab));
                        _viewModel.Funded.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedTDL.FundLab));
                        _viewModel.Expended.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedTDL.ExpLab));
                        _viewModel.FundBalance.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedTDL.FundBalLab));
                        _viewModel.BudgetBalance.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedTDL.BudgBalLab));
                        break;
                    case "Travel":
                        _viewModel.Budget.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedTDL.BudgTrv));
                        _viewModel.Funded.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedTDL.FundTrv));
                        _viewModel.Expended.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedTDL.ExpTrv));
                        _viewModel.FundBalance.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedTDL.FundBalTrv));
                        _viewModel.BudgetBalance.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedTDL.BudgBalTrv));
                        break;
                    case "Material":
                        _viewModel.Budget.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedTDL.BudgMat));
                        _viewModel.Funded.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedTDL.FundMat));
                        _viewModel.Expended.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedTDL.ExpMat));
                        _viewModel.FundBalance.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedTDL.FundBalMat));
                        _viewModel.BudgetBalance.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedTDL.BudgBalMat));
                        break;
                    case "Totals":
                        _viewModel.Budget.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedTDL.BudgTotal));
                        _viewModel.Funded.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedTDL.FundTotal));
                        _viewModel.Expended.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedTDL.ExpTotal));
                        _viewModel.FundBalance.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedTDL.FundBalTotal));
                        _viewModel.BudgetBalance.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedTDL.BudgBalTotal));
                        break;
                    default:
                        break;
                }
           
                }
                if (_viewModel.SelectedTDL != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("TDL ");
                sb.Append(_viewModel.SelectedTDL.TDL_No);
                sb.Append(" : ");
                sb.Append(_viewModel.SelectedTDL.Description);
                TDLChart.Title = sb;
            } 
            
            }

            
     
        
    }
}
