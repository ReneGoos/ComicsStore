using ComicsLibrary.EditModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ComicsLibrary.Navigation;
using System.Windows.Shapes;

namespace ComicsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for SimpleToolbar.xaml
    /// </summary>
    public partial class SimpleToolbar : UserControl
    {
        public SimpleToolbar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ICollectionView), typeof(SimpleToolbar));
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int?), typeof(SimpleToolbar));
        public static readonly DependencyProperty IsCleanProperty = DependencyProperty.Register("IsClean", typeof(bool), typeof(SimpleToolbar));
        public static readonly DependencyProperty IsDirtyProperty = DependencyProperty.Register("IsDirty", typeof(bool), typeof(SimpleToolbar));
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(SimpleToolbar));
        public static readonly DependencyProperty GetCommandProperty = DependencyProperty.Register("GetCommand", typeof(ICommand), typeof(SimpleToolbar));
        public static readonly DependencyProperty NewCommandProperty = DependencyProperty.Register("NewCommand", typeof(ICommand), typeof(SimpleToolbar));
        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register("SaveCommand", typeof(ICommand), typeof(SimpleToolbar));
        public static readonly DependencyProperty CancelSaveCommandProperty = DependencyProperty.Register("CancelSaveCommand", typeof(ICommand), typeof(SimpleToolbar));

        public ICollectionView Items { get { return (ICollectionView)GetValue(ItemsProperty); } set { SetValue(ItemsProperty, value); } }
        public int? Id { get { return (int?)GetValue(IdProperty); } set { SetValue(IdProperty, value); } }
        public bool IsClean { get { return (bool)GetValue(IsCleanProperty); } set { SetValue(IsCleanProperty, value); } }
        public bool IsDirty { get { return (bool)GetValue(IsDirtyProperty); } set { SetValue(IsDirtyProperty, value); } }
        public string Filter { get { return (string)GetValue(FilterProperty); } set { SetValue(FilterProperty, value); } }
        public ICommand GetCommand { get { return (ICommand)GetValue(GetCommandProperty); } set { SetValue(GetCommandProperty, value); } }
        public ICommand NewCommand { get { return (ICommand)GetValue(NewCommandProperty); } set { SetValue(NewCommandProperty, value); } }
        public ICommand SaveCommand { get { return (ICommand)GetValue(SaveCommandProperty); } set { SetValue(SaveCommandProperty, value); } }
        public ICommand CancelSaveCommand { get { return (ICommand)GetValue(CancelSaveCommandProperty); } set { SetValue(CancelSaveCommandProperty, value); } }
    }
}
