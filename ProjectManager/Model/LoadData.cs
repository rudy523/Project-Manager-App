using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace ProjectManager.Model
{
    public class LoadData
    {
        public string FinancialFeed { get; set; }
        public string TaskFeed { get; set; }
        public XElement FinanceRoot { get; set; }
        public XElement TaskRoot { get; set; }
        public IEnumerable<XElement> ProjectNumbers { get; set; }
        public IEnumerable<XAttribute> ProjNumDetails { get; set; }
        public ObservableCollection<ProjectNumber> ProjectNumbersView { get; set; }

        public LoadData()
        {
            GetViewProjectNumbers();
        }

        private void GetFinancialData()
        {
            FinancialFeed = @"\\Scrdata\bah\Rudy\Workbook Development\Programming\Financial Workbook Project Totals.xml";
            XElement FinanceRoot = XElement.Load(FinancialFeed);
            IEnumerable<XElement> ProjectNumbers = FinanceRoot.Descendants();
            IEnumerable<XAttribute> ProjNumDetails = ProjectNumbers.Attributes();
        }

        private void GetProjectNumberDetails(IEnumerable<XAttribute> source, List<string> output)
        {
            if (source.Count() == 41)
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    string value = source.ElementAt(i).Value.ToString();
                    output.Add(value);
                }
            }
            else if (source.Count() == 40)
            {
                for (int n = 0; n < 40; n++)
                {
                    string value = source.ElementAt(n).Value.ToString();
                    output.Add(value);
                }
                output.Insert(5, "0");
            }
            else
            {
                throw new IndexOutOfRangeException();
            }          
        }

        private ProjectNumber CreateProjectNumber(List<string> input)
        {
            ProjectNumber num = new ProjectNumber(input.ElementAt(0), input.ElementAt(1), input.ElementAt(2), int.Parse(input.ElementAt(3)), int.Parse(input.ElementAt(4)), int.Parse(input.ElementAt(5)),
                input.ElementAt(6), int.Parse(input.ElementAt(7)), input.ElementAt(8), input.ElementAt(9), input.ElementAt(10), input.ElementAt(11), input.ElementAt(12), input.ElementAt(13),
                decimal.Parse(input.ElementAt(14)), decimal.Parse(input.ElementAt(15)), decimal.Parse(input.ElementAt(16)), decimal.Parse(input.ElementAt(17)), decimal.Parse(input.ElementAt(18)),
                decimal.Parse(input.ElementAt(19)), decimal.Parse(input.ElementAt(20)), decimal.Parse(input.ElementAt(21)), decimal.Parse(input.ElementAt(22)), decimal.Parse(input.ElementAt(23)),
                decimal.Parse(input.ElementAt(24)), decimal.Parse(input.ElementAt(25)), decimal.Parse(input.ElementAt(26)), decimal.Parse(input.ElementAt(27)), decimal.Parse(input.ElementAt(28)),
                decimal.Parse(input.ElementAt(29)), decimal.Parse(input.ElementAt(30)), decimal.Parse(input.ElementAt(31)), decimal.Parse(input.ElementAt(32)), decimal.Parse(input.ElementAt(33)),
                decimal.Parse(input.ElementAt(34)), decimal.Parse(input.ElementAt(35)), decimal.Parse(input.ElementAt(36)), decimal.Parse(input.ElementAt(37)), DateTime.Parse(input.ElementAt(38)),
                DateTime.Parse(input.ElementAt(39)), DateTime.Parse(input.ElementAt(40)));

            return num;
        }

        private void GetViewProjectNumbers()
        {
            GetFinancialData();

            foreach (var item in ProjectNumbers)
            {
                    IEnumerable<XAttribute> rawData = item.Attributes();

                    if (rawData.Count() < 40)
                    {
                        continue;
                    }

                    List<string> Data = new List<string>();

                    GetProjectNumberDetails(rawData, Data);

                    ProjectNumber num = CreateProjectNumber(Data);

                    this.ProjectNumbersView.Add(num);
            }
        }

        private void SearchProjectNumbers(List<string> Criteria)
        {
            
            GetFinancialData();

            foreach (var item in Criteria)
            {
                IEnumerable<XElement> matches = from number in ProjectNumbers
                                                where number.Value == item
                                                select number.Parent;
                foreach (var line in matches)
                {
                    IEnumerable<XAttribute> rawData = line.Attributes();

                    if (rawData.Count() < 40)
                    {
                        continue;
                    }

                    List<string> Data = new List<string>();

                    GetProjectNumberDetails(rawData, Data);

                    ProjectNumber num = CreateProjectNumber(Data);

                    ProjectNumbersView.Add(num);

                }
            }
        }

    }
}
