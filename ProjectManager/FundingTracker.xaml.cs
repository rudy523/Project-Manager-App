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
using System.Windows.Forms;
using System.Linq;
using System.Collections.ObjectModel;
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
            Microsoft.Win32.SaveFileDialog saveDlg = new Microsoft.Win32.SaveFileDialog();
            saveDlg.RestoreDirectory = true;
            saveDlg.ShowDialog();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel = SaveLoadState.Load("SaveData.xml");
            _viewModel.ReinstateParentObjects();
            DataContext = _viewModel;
            FundingGrid.Visibility = Visibility.Visible;
            //MIPRdisplay.Visibility = Visibility.Visible;
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {

            this.SearchCriteria = SearchBox.Text.Split(' ');
            _viewModel.SearchData(SearchCriteria);
            //base.DataContext = _viewModel;

            //MIPRdisplay.ItemsSource = _viewModel.MIPRnums;
            //MIPRdisplay.Visibility = Visibility.Visible;         
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
            //MIPRdisplay.Visibility = Visibility.Hidden;
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

        private void MIPRFilter_Click(object sender, RoutedEventArgs e)
        {
            //   DateTime endDate
            _viewModel.MIPRList.Clear();
            _viewModel.miprsum.Clear();
            _viewModel.ProjNumList.Clear();
            bool current = false;
            bool useWCD = false;
            DateTime WCD = DateTime.Parse("1/1/1900");

            if (FundCurrentCheck.IsChecked == true)
            {
                current = true;
            }

            if (FundCustomDateCheck.IsChecked == true)
            {
                useWCD = true;
                WCD = (DateTime)WCDPick.SelectedDate;
            }

            List<string> SelectedEngineers = new List<string>();
            foreach (var item in FundEngList.SelectedItems)
            {
                SelectedEngineers.Add(item.ToString());
            }
            _viewModel.myFunding.GenerateMIPRList(SelectedEngineers, current, useWCD, WCD);

            if (_viewModel.myFunding.MIPRs.Count == 0)
            {
                MIPRSummaryViewModel noresult = new MIPRSummaryViewModel("No Results! Check Search Parameters");
                _viewModel.miprsum.Add(noresult);
            }
            else
            {
                foreach (var item in _viewModel.myFunding.MIPRs)
                {
                    _viewModel.MIPRList.Add(item);
                }
                foreach (var item in _viewModel.MIPRList)
                {
                    _viewModel.miprsum.Add(new MIPRSummaryViewModel(item));
                }
            }
        }

        private void FundCurrentCheck_Click(object sender, RoutedEventArgs e)
        {
            FundCustomDateCheck.IsChecked = false;
            WCDPick.IsEnabled = false;
        }

        private void FundCustomDateCheck_Click(object sender, RoutedEventArgs e)
        {
            FundCurrentCheck.IsChecked = false;
            WCDPick.IsEnabled = true;
        }

        private void Show_Nums_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ProjNumList.Clear();
            List<string> SelectedMIPRs = new List<string>();
            foreach (var num in MIPRList.SelectedItems)
            {
                MIPRSummaryViewModel mipr = (MIPRSummaryViewModel)num;
                string miprnum = mipr.MIPRnum;
                foreach (var item in _viewModel.MIPRList)
                {
                    if (item.MIPRnum == miprnum)
                    {
                        foreach (var projnum in item.ProjectNums)
                        {
                            _viewModel.ProjNumList.Add(projnum);
                        }
                    }
                }
            }
        }

        private void Track_Nums_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProjNumList.SelectedItems.Count > 0)
            {
                
                List<ProjNum> tracklist = new List<ProjNum>();
                // Move selected project numbers to Tracked Project Numbers
                foreach (var item in ProjNumList.SelectedItems)
                {
                    ProjNum num = (ProjNum)item;
                    _viewModel.TrackedProjNums.Add(num);
                    tracklist.Add(num);
                }
                // Generate MIPR summary for selected project numbers
                _viewModel.TrackedMIPR.Add(new MIPRSummaryViewModel(tracklist));
                List<MIPRSummaryViewModel> miprsum = new List<MIPRSummaryViewModel>();
                int counter = _viewModel.TrackedMIPR.Count();
                if (counter == 1)
                {
                    foreach (var item in _viewModel.TrackedMIPR)
                    {
                        miprsum.Add(item);
                    }
                    _viewModel.TrackedMIPR.Add(new MIPRSummaryViewModel(miprsum));
                }
                else if (_viewModel.TrackedMIPR.ElementAt(counter - 2).MIPRnum == "Project Totals")
                {
                    _viewModel.TrackedMIPR.RemoveAt(_viewModel.TrackedMIPR.Count() - 2);
                    foreach (var item in _viewModel.TrackedMIPR)
                    {
                        miprsum.Add(item);
                    }
                    _viewModel.TrackedMIPR.Add(new MIPRSummaryViewModel(miprsum));
                }

                // Unhide UI elements
                FundsGrid.Visibility = Visibility.Visible;
                TrackedFunds.Visibility = Visibility.Visible;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Select Project Numbers to Track");
            }
        
           
        }

        private void Fund_Reset_Param_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MIPRList.Clear();
            _viewModel.miprsum.Clear();
            _viewModel.ProjNumList.Clear();
            FundCurrentCheck.IsChecked = false;
            FundCustomDateCheck.IsChecked = false;
            WCDPick.IsEnabled = false;
            FundEngList.SelectedItems.Clear();
            MIPRList.SelectedItem = null;
            ProjNumList.SelectedItems.Clear();
        }

        private void TrackedMIPRdisplay_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            bool miprSelected = false;
            try
            {
                MIPRSummaryViewModel selection = (MIPRSummaryViewModel)TrackedMIPRdisplay.SelectedItem;
                _viewModel.SelectedMIPR = selection;
                miprSelected = true;
            }
            catch (Exception)
            {
                ProjNum selection = (ProjNum)TrackedMIPRdisplay.SelectedItem;
                _viewModel.SelectedProjNum = selection;
            }

            if (_viewModel.SelectedMIPR != null & miprSelected == true)
            {
                _viewModel.FundingFunded.Clear();
                _viewModel.FundingExpended.Clear();
                _viewModel.FundingBalance.Clear();

                switch (_viewModel.FundingSelectedCat)
                {
                    case "Labor":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedMIPR.LabAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedMIPR.LabExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedMIPR.LabBal));
                        break;
                    case "Travel":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedMIPR.TrvAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedMIPR.TrvExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedMIPR.TrvBal));
                        break;
                    case "Material":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedMIPR.MatAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedMIPR.MatExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedMIPR.MatBal));
                        break;
                    case "Services":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedMIPR.SvcAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedMIPR.SvcExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedMIPR.SvcBal));
                        break;
                    case "Division":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedMIPR.DivAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedMIPR.DivExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedMIPR.DivBal));
                        break;
                    case "CB":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedMIPR.CBAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedMIPR.CBExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedMIPR.CBBal));
                        break;
                    case "Other":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedMIPR.OtherAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedMIPR.OtherExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedMIPR.OtherBal));
                        break;
                    case "Total":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedMIPR.TotalAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedMIPR.TotalExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedMIPR.TotalBal));
                        break;
                    default:
                        break;
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(_viewModel.SelectedMIPR.MIPRnum);
                sb.Append(" : ");
                sb.Append(_viewModel.SelectedMIPR.Sponsor);
                MIPRSnapChart.Title = sb;
            }
            else if (_viewModel.SelectedProjNum != null)
            {
                _viewModel.FundingFunded.Clear();
                _viewModel.FundingExpended.Clear();
                _viewModel.FundingBalance.Clear();

                switch (_viewModel.FundingSelectedCat)
                {
                    case "Labor":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedProjNum.LabAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedProjNum.LabExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedProjNum.LabBal));
                        break;
                    case "Travel":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedProjNum.TrvAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedProjNum.TrvExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedProjNum.TrvBal));
                        break;
                    case "Material":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedProjNum.MatAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedProjNum.MatExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedProjNum.MatBal));
                        break;
                    case "Services":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedProjNum.SvcAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedProjNum.SvcExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedProjNum.SvcBal));
                        break;
                    case "Division":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedProjNum.DivAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedProjNum.DivExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedProjNum.DivBal));
                        break;
                    case "CB":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedProjNum.CBAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedProjNum.CBExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedProjNum.CBBal));
                        break;
                    case "Other":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedProjNum.OtherAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedProjNum.OtherExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedProjNum.OtherBal));
                        break;
                    case "Total":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedProjNum.TotalAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedProjNum.TotalExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedProjNum.TotalBal));
                        break;
                    default:
                        break;
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(_viewModel.SelectedProjNum.Projnum);
                sb.Append(" : ");
                sb.Append(_viewModel.SelectedProjNum.Title);
                MIPRSnapChart.Title = sb;
            }
        }

        private void FundCategoryPick_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_viewModel.SelectedMIPR != null)
            {
                _viewModel.FundingFunded.Clear();
                _viewModel.FundingExpended.Clear();
                _viewModel.FundingBalance.Clear();

                switch (_viewModel.FundingSelectedCat)
                {
                    case "Labor":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedMIPR.LabAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedMIPR.LabExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Labor", _viewModel.SelectedMIPR.LabBal));
                        break;
                    case "Travel":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedMIPR.TrvAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedMIPR.TrvExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Travel", _viewModel.SelectedMIPR.TrvBal));
                        break;
                    case "Material":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedMIPR.MatAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedMIPR.MatExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Material", _viewModel.SelectedMIPR.MatBal));
                        break;
                    case "Services":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedMIPR.SvcAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedMIPR.SvcExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Services", _viewModel.SelectedMIPR.SvcBal));
                        break;
                    case "Division":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedMIPR.DivAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedMIPR.DivExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Division", _viewModel.SelectedMIPR.DivBal));
                        break;
                    case "CB":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedMIPR.CBAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedMIPR.CBExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("CB", _viewModel.SelectedMIPR.CBBal));
                        break;
                    case "Other":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedMIPR.OtherAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedMIPR.OtherExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Other", _viewModel.SelectedMIPR.OtherBal));
                        break;
                    case "Total":
                        _viewModel.FundingFunded.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedMIPR.TotalAlloc));
                        _viewModel.FundingExpended.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedMIPR.TotalExp));
                        _viewModel.FundingBalance.Add(new KeyValuePair<string, decimal>("Total", _viewModel.SelectedMIPR.TotalBal));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
