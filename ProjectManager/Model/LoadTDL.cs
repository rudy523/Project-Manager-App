using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using ProjectManager.ViewModels;

namespace ProjectManager.Model
{
    public class LoadTDL
    {
        #region Properties
        // Source Data
        public IEnumerable<XElement> SourceData { get; set; }
        // Lists for filtering
        public ObservableCollection<string> EngList { get; set; }
        public ObservableCollection<string> ContractList { get; set; }
        public ObservableCollection<TDLViewModel> FilteredTDLs { get; set; }


        #endregion
        #region Constructors

        public LoadTDL()
        {
            // Loads source data to run queries against
            LoadSourceData();
            EngList = new ObservableCollection<string>();
            ContractList = new ObservableCollection<string>();
            getTDLdata();
        }
        #endregion

        #region Methods
        private void LoadSourceData()
        {
            // Sets location of source data file
            string FinancialFeed = @"\\Scrdata\bah\Rudy\Workbook Development\Programming\Financial Workbook TDLs.xml";
            // Loads source data into XElement. This will be the source to run queries against. NOTE: May require conversion to IEnumberable.
            XElement Root = XElement.Load(FinancialFeed);
            this.SourceData = Root.Descendants();
        }


        private void getTDLdata()
        {
            FilteredTDLs = new ObservableCollection<TDLViewModel>();
            List<string> allContracts = new List<string>();
            List<string> allEngineers = new List<string>();     
            for (int i = 2; i < SourceData.Count(); i++ )
            {
                IEnumerable<XAttribute> data = SourceData.ElementAt(i).Attributes();
                allContracts.Add(data.ElementAt(1).Value.ToString());
                allEngineers.Add(data.ElementAt(5).Value.ToString());
            }
            allContracts.Sort();
            allEngineers.Sort();
            IEnumerable<string> distinctContracts = allContracts.Distinct();    
            IEnumerable<string> distinctEngineers = allEngineers.Distinct();
            foreach (var item in distinctContracts)
            {
                ContractList.Add(item);
            }
            foreach (var item in distinctEngineers)
            {
                EngList.Add(item);
            }
        }


        public void RunQuery(List<string> Engineers, List<string> Contracts, bool Current, DateTime startDate, DateTime endDate)
        {
            this.FilteredTDLs.Clear();
            bool useStartdate = false;
            bool useFinishDate = false;
            if (startDate != DateTime.Parse("1/1/1900"))
            {
                useStartdate = true;
            }
            if (endDate != DateTime.Parse("1/1/1900"))
            {
                useFinishDate = true;
            }

            List<TDLViewModel> engResults = new List<TDLViewModel>();
            List<TDLViewModel> contractResults = new List<TDLViewModel>();
            var TDLattributes = SourceData.Attributes();

            foreach (var item in Engineers)
            {        
                var EngineerSearch = from match in TDLattributes
                                     where match.Value == item
                                     select match.Parent;
                foreach (var num in EngineerSearch)
                {
                    TDLViewModel eng = new TDLViewModel(num);
                    engResults.Add(eng);
                }
            }

            foreach (var item in Contracts)
            {
                var ContractSearch = from match in TDLattributes
                                     where match.Value == item
                                     select match.Parent;
                foreach (var num in ContractSearch)
                {
                    TDLViewModel cont = new TDLViewModel(num);
                    contractResults.Add(cont);
                }
            }

            TDLcomparer compare = new TDLcomparer();

            if (engResults.Count > 0 && contractResults.Count == 0)
            {
                contractResults = engResults;
            }
            else if (engResults.Count == 0 && contractResults.Count > 0)
            {
                engResults = contractResults;
            }

            var combinedResults = engResults.Intersect(contractResults, compare);

            foreach (var item in combinedResults)
            {
                FilteredTDLs.Add(item);
            }

            if (Current == true)
            {
                FilteredTDLs.Clear();
                var currentTasks =  from match in combinedResults
                                    where match.CompDate >= DateTime.Today
                                    select match;
                int count = currentTasks.Count();
                if (count > 0)
                {
                    foreach (var item in currentTasks)
                    {
                        FilteredTDLs.Add(item);
                    }
                }
            }

            if (useStartdate == true && useFinishDate == true)
            {
                FilteredTDLs.Clear();
                var selectedstartTasks = from match in combinedResults
                                    where match.StartDate >= startDate
                                    select match;

                var selectedendTasks = from match in combinedResults
                                    where match.CompDate <= endDate
                                    select match;

                var combinedTasks = selectedstartTasks.Intersect(selectedendTasks, compare);

                foreach (var item in combinedTasks)
                {
                    FilteredTDLs.Add(item);
                }
            }

            else if (useStartdate == true)
            {
                FilteredTDLs.Clear();
                var selectedTasks = from match in combinedResults
                                    where match.StartDate >= startDate
                                    select match;

                foreach (var item in selectedTasks)
                {
                    FilteredTDLs.Add(item);
                }
            }

            else if (useFinishDate == true)
            {
                FilteredTDLs.Clear();
                var selectedTasks = from match in combinedResults
                                    where match.CompDate <= endDate
                                    select match;

                foreach (var item in selectedTasks)
                {
                        FilteredTDLs.Add(item);  
                }         
            }
        }
        #endregion

    }
}
