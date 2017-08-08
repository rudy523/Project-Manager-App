using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManager.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Windows.Data;
using System;



namespace ProjectManager.ViewModels
{
    [Serializable]
    public class MainViewModel 
    {
        private LoadData MyData { get; set; }
        private ObservableCollection<MIPR> ProjectNumbers { get; set; }
        private event PropertyChangedEventHandler PropertyChanged;
        [XmlElement]
        public ObservableCollection<MIPRViewModel> MIPRnums { get; set; }
        [XmlElement]
        public ObservableCollection<DataGridViewModel> GridData { get; set; }
        [XmlElement]
        public ObservableCollection<ChartDataViewModel> ChartDataModel { get; set; }
        private ObservableCollection<MIPRViewModel> UpdateMIPRnums { get; set; }
        public CollectionViewSource GridView { get; set; }


        public MainViewModel()    
        {
            this.ProjectNumbers = new ObservableCollection<MIPR>();
            this.GridData = new ObservableCollection<DataGridViewModel>();
            this.ChartDataModel = new ObservableCollection<ChartDataViewModel>();
            this.MIPRnums = new ObservableCollection<MIPRViewModel>();
            this.UpdateMIPRnums = new ObservableCollection<MIPRViewModel>();
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

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
