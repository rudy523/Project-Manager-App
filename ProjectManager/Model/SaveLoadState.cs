using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProjectManager.ViewModels;
using System.IO;
using System.Collections.ObjectModel;

namespace ProjectManager.Model
{   
    [Serializable]      
    public class SaveLoadState
    {
        public static void Save(string Filename, object input)
        {
            using (var writer = new StreamWriter(Filename))
            {
                var serializer = new XmlSerializer(input.GetType());
                serializer.Serialize(writer, input);
                writer.Flush();
            }
        }

        public static ObservableCollection<ChartDataViewModel> Load(string Filename)
        {
            using (var loader = new StreamReader(Filename))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<ChartDataViewModel>));
                return serializer.Deserialize(loader) as ObservableCollection<ChartDataViewModel>;
            }
        }

        public static ObservableCollection<DataGridViewModel> GridLoad(string Filename)
        {
            using (var loader = new StreamReader(Filename))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<DataGridViewModel>));
                return serializer.Deserialize(loader) as ObservableCollection<DataGridViewModel>;
            }
        }

        public static ObservableCollection<MIPRViewModel> MIPRload(string Filename)
        {
            using (var loader = new StreamReader(Filename))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<MIPRViewModel>));
                return serializer.Deserialize(loader) as ObservableCollection<MIPRViewModel>;
            }
        }
    }
}
