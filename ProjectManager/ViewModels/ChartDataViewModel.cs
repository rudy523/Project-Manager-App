using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjectManager.ViewModels
{   
    [Serializable]
    public class ChartDataViewModel
    {      
        public decimal FundedPointValue { get; set; }
        public decimal ExpendedPointValue { get; set; }    
        public decimal BalancePointValue { get; set; }
        public DateTime Date { get; set; }

        public ChartDataViewModel() { }
        public ChartDataViewModel(decimal fundedpointValue, decimal expendedpointValue, decimal balancepointValue, DateTime date)
        {
            FundedPointValue = fundedpointValue;
            ExpendedPointValue = expendedpointValue;
            BalancePointValue = balancepointValue;
            Date = date;                 
        }
        
    }
}
