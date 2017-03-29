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

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ProjectFunding : Page
    {
        public MainViewModel ViewModel { get; set; }

        public ProjectFunding()
        {
            this.InitializeComponent();
            this.ViewModel = new MainViewModel();
        }

        
    }
}
