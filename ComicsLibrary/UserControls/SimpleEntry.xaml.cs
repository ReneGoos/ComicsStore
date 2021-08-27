using System.Windows;
using System.Windows.Controls;

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
            ItemCase = CharacterCasing.Normal;
        }

        public static readonly DependencyProperty ItemIdProperty = DependencyProperty.Register("Id", typeof(int?), typeof(SimpleEntry));
        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register("ItemName", typeof(string), typeof(SimpleEntry));
        public static readonly DependencyProperty ItemCaseProperty = DependencyProperty.Register("ItemCase", typeof(CharacterCasing), typeof(SimpleEntry));
        public static readonly DependencyProperty ItemRemarkProperty = DependencyProperty.Register("ItemRemark", typeof(string), typeof(SimpleEntry));
        public static readonly DependencyProperty ItemErrorProperty = DependencyProperty.Register("ItemError", typeof(string), typeof(SimpleEntry));

        public int Id { get => (int)GetValue(ItemIdProperty); set => SetValue(ItemIdProperty, value); }
        public string ItemName { get => (string)GetValue(ItemNameProperty); set => SetValue(ItemNameProperty, value); }
        public CharacterCasing ItemCase { get => (CharacterCasing)GetValue(ItemCaseProperty); set => SetValue(ItemCaseProperty, value); }
        public string ItemRemark { get => (string)GetValue(ItemRemarkProperty); set => SetValue(ItemRemarkProperty, value); }
        public string ItemError { get => (string)GetValue(ItemErrorProperty); set => SetValue(ItemErrorProperty, value); }
    }
}
