﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManager.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System;

namespace ProjectManager.ViewModels
{
    [Serializable]
    public class MainViewModel : ObservableCollection<KeyValuePair<string, decimal>>
    {
        #region Properties
        private LoadData MyData { get; set; }
        private ObservableCollection<MIPR> ProjectNumbers { get; set; }
        private ObservableCollection<MIPRViewModel> UpdateMIPRnums { get; set; }
        private event PropertyChangedEventHandler PropertyChanged;
        [XmlElement]
        public ObservableCollection<MIPRViewModel> MIPRnums { get; set; }
        [XmlElement]
        public ObservableCollection<DataGridViewModel> GridData { get; set; }
        [XmlElement]
        public ObservableCollection<ChartDataViewModel> ChartDataModel { get; set; }
        [XmlElement]
        public ObservableCollection<MIPRSummaryViewModel> MIPRsummary { get; set; }
        //TDL objects
        public ObservableCollection<string> EngineerList { get; set; }
        public ObservableCollection<string> ContractList { get; set; }
        public ObservableCollection<TDLViewModel> filteredTDLs { get; set; }
        public ObservableCollection<TDLViewModel> TrackedTDLs { get; set; }
        public LoadTDL myTDL { get; set; }
        public string SelectedCat { get; set; }
        public TDLViewModel SelectedTDL { get; set; }
        public ObservableCollection<KeyValuePair<string,decimal>> Budget { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> Funded { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> Expended { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> FundBalance { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> BudgetBalance { get; set; }
        //Funding revamp
        public LoadData myFunding { get; set; }
        public ObservableCollection<MIPR> MIPRList { get; set; }
        public ObservableCollection<MIPRSummaryViewModel> miprsum { get; set; }
        public ObservableCollection<ProjNum> ProjNumList { get; set; }
        public ObservableCollection<ProjNum> TrackedProjNums { get; set; }
        public MIPRSummaryViewModel SelectedMIPR { get; set; }
        public ProjNum SelectedProjNum { get; set; }
        public string FundingSelectedCat { get; set; }
        public ObservableCollection<MIPRSummaryViewModel> TrackedMIPR { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> FundingFunded { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> FundingExpended { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> FundingBalance { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> WRfund { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> DCfund { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal>> POfund { get; set; }
        public List<string> Tester { get; set; }


        #endregion

        public MainViewModel()    
        {
            this.ProjectNumbers = new ObservableCollection<MIPR>();
            this.GridData = new ObservableCollection<DataGridViewModel>();
            this.ChartDataModel = new ObservableCollection<ChartDataViewModel>();
            this.MIPRnums = new ObservableCollection<MIPRViewModel>();
            this.UpdateMIPRnums = new ObservableCollection<MIPRViewModel>();
            MIPRsummary = new ObservableCollection<MIPRSummaryViewModel>();
            TDLsetup();
            // Funding revamp
            this.myFunding = new LoadData();
            MIPRList = new ObservableCollection<MIPR>();
            this.ProjNumList = new ObservableCollection<ProjNum>();
            this.miprsum = new ObservableCollection<MIPRSummaryViewModel>();
            this.TrackedProjNums = new ObservableCollection<ProjNum>();
            this.FundingFunded = new ObservableCollection<KeyValuePair<string, decimal>>();
            this.FundingExpended = new ObservableCollection<KeyValuePair<string, decimal>>();
            this.FundingBalance = new ObservableCollection<KeyValuePair<string, decimal>>();
            this.WRfund = new ObservableCollection<KeyValuePair<string, decimal>>();
            this.DCfund = new ObservableCollection<KeyValuePair<string, decimal>>();
            this.POfund = new ObservableCollection<KeyValuePair<string, decimal>>();
            //Dashboard
            this.TrackedMIPR = new ObservableCollection<MIPRSummaryViewModel>();
            //Testing
            this.Tester = new List<string>();
        }

        private void TDLsetup()
        {
            myTDL = new LoadTDL();
            EngineerList = new ObservableCollection<string>();
            ContractList = new ObservableCollection<string>();
            filteredTDLs = new ObservableCollection<TDLViewModel>();
            TrackedTDLs = new ObservableCollection<TDLViewModel>();
            Budget = new ObservableCollection<KeyValuePair<string, decimal>>();
            Funded = new ObservableCollection<KeyValuePair<string, decimal>>();
            Expended = new ObservableCollection<KeyValuePair<string, decimal>>();
            FundBalance = new ObservableCollection<KeyValuePair<string, decimal>>();
            BudgetBalance = new ObservableCollection<KeyValuePair<string, decimal>>();

            EngineerList = myTDL.EngList;
            ContractList = myTDL.ContractList;
        }

        public void SearchData(string[] input)
        {
            ProjectNumbers.Clear();
            MIPRnums.Clear();
            this.MyData = new LoadData(input);
            foreach (var item in MyData.MIPRcollection)
            {
                ProjectNumbers.Add(item);
            }
            foreach (var item in this.ProjectNumbers)
            {
                MIPRViewModel num = new MIPRViewModel(item);
                MIPRnums.Add(num);
            }
        }

        public void TrackFunding()
        {
            if (GridData.Count() != 0)
            {
                GridData.RemoveAt(GridData.Count() - 1);
            }
            foreach (var item in MIPRnums)
            {
                foreach (var number in item.MIPRchildren)
                {
                    if (number.IsChecked == true)
                    {
                        int counter = 0;
                        DataGridViewModel selection = new DataGridViewModel(number.ParentNumber, number.ProjNum, number);
                        foreach (var data in GridData)
                        {
                            if (selection.ProjNum == data.ProjNum)
                            {
                                counter++;
                            }
                        }
                        if (counter == 0)
                        {
                            GridData.Add(selection);                          
                        }
                    }
                }
            }
            this.UpdateGraph();

            // Gets list of distinct MIPR numbers
            List<string> MIPRnames = new List<string>();

            foreach (var item in GridData)
            {
                MIPRnames.Add(item.MIPRnum);
            }

            IEnumerable<string> nums = MIPRnames.Distinct();

            MIPRnames = nums.ToList();

            foreach (var item in MIPRnames)
            {
                IEnumerable<DataGridViewModel> matches = from match in GridData
                                                         where match.MIPRnum == item
                                                         select match;
                MIPRSummaryViewModel summary = new MIPRSummaryViewModel(item, matches);

                if (MIPRsummary.Count == 0)
                {
                    MIPRsummary.Add(summary);
                }
                else
                {
                    int counter = 0;
                    foreach (var num in MIPRsummary)
                    {
                        if (num.MIPRnum == summary.MIPRnum)
                        {
                            counter++;
                        }
                    }
                    switch (counter)
                    {
                        case 0:
                            MIPRsummary.Add(summary);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void UpdateFunding()
        {
            string[] updateNums = new string[GridData.Count()];
            MyData.UpdateNumbers.Clear();
            for (int i = 0; i < GridData.Count(); i++)
            {
                MyData.UpdateNumbers.Add(GridData.ElementAt(i).ProjNum);
            }
            MyData.UpdateFunding();
            GridData.Clear();
            foreach (var item in MyData.UpdateMIPRcollection)
            {
                MIPRViewModel num = new MIPRViewModel(item);
                UpdateMIPRnums.Add(num);
            }
            foreach (var item in UpdateMIPRnums)
            {
                foreach (var number in item.MIPRchildren)
                {
                    DataGridViewModel selection = new DataGridViewModel(number.ParentNumber, number.ProjNum, number);
                    GridData.Add(selection);
                }
            }
            this.UpdateGraph();

        }

        private void UpdateGraph()
        { 
            ChartDataViewModel results = DataGridViewModel.GetTotals(GridData);

            if (ChartDataModel.Count == 0)
            {
                ChartDataModel.Add(results);
               
            }
            else
            {
                int lastChartset = ChartDataModel.Count - 1;
                if (results.Date.Month == ChartDataModel[lastChartset].Date.Month)
                {
                    ChartDataModel.RemoveAt(lastChartset);
                    ChartDataModel.Add(results);
                   
                }
                else
                {
                    ChartDataModel.Add(results);
                  
                }
            }
        }

        public void ReinstateParentObjects()
        {
            foreach (var item in MIPRnums)
            {
                if (item.IsChecked == false)
                {
                    item.IsExpanded = false;
                }
                foreach (var num in item.MIPRchildren)
                {
                    if (num.IsChecked)
                    {
                        item.IsExpanded = true;
                    }
                }         
                foreach (var num in item.MIPRchildren)
                {
                    num.Parent = item;
                }
            }
        }

      

       

    }
}
