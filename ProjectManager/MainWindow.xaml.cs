using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectManager.Model;
using System.Collections.ObjectModel;

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MIPRNumber> MyData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TrackFunding_Click(object sender, RoutedEventArgs e)
        {
            FundingTracker tracker = new FundingTracker();
            tracker.Show();
        }
    }
}
