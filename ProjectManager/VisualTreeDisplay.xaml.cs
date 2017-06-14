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

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for VisualTreeDisplay.xaml
    /// </summary>
    public partial class VisualTreeDisplay : System.Windows.Window
    {

        public VisualTreeDisplay()
        {
            InitializeComponent();
        }

        public void ShowVisualTree(DependencyObject element)
        {
            // clear the tree
            TreeDisplay.Items.Clear();

            // start processing elements, beginning at the root
            ProcessElement(element, null);

            DataContext = this;
        }

        private void ProcessElement(DependencyObject element, TreeViewItem previousItem)
        {
            // create a TreeViewItem for the current element
            TreeViewItem item = new TreeViewItem();
            item.Header = element.GetType().Name;
            item.IsExpanded = true;

            // check whether item should be added to root of the tree. Yes if it is the first, nest under another if not
            if (previousItem == null)
            {
                TreeDisplay.Items.Add(item);
            }
            else
            {
                previousItem.Items.Add(item);
            }

            // check whether this element contains other elements
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                // process each contained element recursively
                ProcessElement(VisualTreeHelper.GetChild(element, i), item);
            }
        }
    }
}
