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

namespace ComicsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for SimpleEntry.xaml
    /// </summary>
    public partial class SimpleEntry : UserControl
    {
        public SimpleEntry()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemIdProperty = DependencyProperty.Register("Id", typeof(int?), typeof(SimpleEntry));
        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register("ItemName", typeof(string), typeof(SimpleEntry));
        public static readonly DependencyProperty ItemRemarkProperty = DependencyProperty.Register("ItemRemark", typeof(string), typeof(SimpleEntry));

        public int Id { get { return (int)GetValue(ItemIdProperty); } set { SetValue(ItemIdProperty, value); } }
        public string ItemName { get { return (string)GetValue(ItemNameProperty); } set { SetValue(ItemNameProperty, value); } }
        public string ItemRemark { get { return (string)GetValue(ItemRemarkProperty); } set { SetValue(ItemRemarkProperty, value); } }
    }
}
