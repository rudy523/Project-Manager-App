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
using System.Windows.Shapes;
using ProjectManager.ViewModels;

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public List<int> TestNumbers { get; set; }

        public TestWindow()
        {
            InitializeComponent();
            TestingClass test = new TestingClass();
            TestNumbers = test.myNumbers;
            DataContext = this;
        }
    }
}
