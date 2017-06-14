using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManager.Model;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

namespace ProjectManager.ViewModels
{
    [Serializable]
    public class MainViewModel 
    {
        public LoadData MyData { get; set; }
        public ObservableCollection<MIPR> ProjectNumbers { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;        
        public ObservableCollection<MIPRViewModel> MIPRnums { get; set; }
        public ObservableCollection<DataGridViewModel> GridData { get; set; }
        public ChartDataViewModel ChartData { get; set; }
        public ObservableCollection<ChartDataViewModel> ChartDataModel { get; set; }

        public MainViewModel() { }

        public MainViewModel(string[] input)    
        {
            this.MyData = new LoadData(input);
            this.ProjectNumbers = this.MyData.MIPRcollection;        
            this.GridData = new ObservableCollection<DataGridViewModel>();
            this.ChartData = new ChartDataViewModel();
            this.ChartDataModel = new ObservableCollection<ChartDataViewModel>();
            this.MIPRnums = new ObservableCollection<MIPRViewModel>();
            List<MIPRViewModel> nums = new List<MIPRViewModel>();
            foreach (var item in this.ProjectNumbers)
            {
                MIPRViewModel num = new MIPRViewModel(item);
                MIPRnums.Add(num);
            }
        }

        public void UpdateFunding()
        {
            
        }

        public void TrackFunding()
        {
            GridData.Clear();
            foreach (var item in MIPRnums)
            {
                foreach (var number in item.MIPRchildren)
                {
                    if (number.IsChecked == true)
                    {
                        DataGridViewModel selection = new DataGridViewModel(number.ParentNumber, number.ProjNum, number);
                        GridData.Add(selection);
                    }                                                                    
                }      
            }

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

            /*
            DataGridViewModel.GetTotals(GridData, ChartDataModel);
            ChartData = ChartDataModel[0];
            */
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

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
