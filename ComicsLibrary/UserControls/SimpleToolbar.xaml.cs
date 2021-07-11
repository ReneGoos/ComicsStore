using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static readonly DependencyProperty FilteredItemsProperty = DependencyProperty.Register("FilteredItems", typeof(ICollectionView), typeof(SimpleToolbar));
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int?), typeof(SimpleToolbar));
        public static readonly DependencyProperty IsCleanProperty = DependencyProperty.Register("IsClean", typeof(bool), typeof(SimpleToolbar));
        public static readonly DependencyProperty IsDirtyProperty = DependencyProperty.Register("IsDirty", typeof(bool), typeof(SimpleToolbar));
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(SimpleToolbar));
        public static readonly DependencyProperty GetCommandProperty = DependencyProperty.Register("GetCommand", typeof(ICommand), typeof(SimpleToolbar));
        public static readonly DependencyProperty NewCommandProperty = DependencyProperty.Register("NewCommand", typeof(ICommand), typeof(SimpleToolbar));
        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register("SaveCommand", typeof(ICommand), typeof(SimpleToolbar));
        public static readonly DependencyProperty UndoCommandProperty = DependencyProperty.Register("UndoCommand", typeof(ICommand), typeof(SimpleToolbar));
        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(SimpleToolbar));

        public ICollectionView FilteredItems { get => (ICollectionView)GetValue(FilteredItemsProperty); set => SetValue(FilteredItemsProperty, value); }
        public int? Id { get => (int?)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public bool IsClean { get => (bool)GetValue(IsCleanProperty); set => SetValue(IsCleanProperty, value); }
        public bool IsDirty { get => (bool)GetValue(IsDirtyProperty); set => SetValue(IsDirtyProperty, value); }
        public string Filter { get => (string)GetValue(FilterProperty); set => SetValue(FilterProperty, value); }
        public ICommand GetCommand { get => (ICommand)GetValue(GetCommandProperty); set => SetValue(GetCommandProperty, value); }
        public ICommand NewCommand { get => (ICommand)GetValue(NewCommandProperty); set => SetValue(NewCommandProperty, value); }
        public ICommand SaveCommand { get => (ICommand)GetValue(SaveCommandProperty); set => SetValue(SaveCommandProperty, value); }
        public ICommand UndoCommand { get => (ICommand)GetValue(UndoCommandProperty); set => SetValue(UndoCommandProperty, value); }
        public ICommand DeleteCommand { get => (ICommand)GetValue(DeleteCommandProperty); set => SetValue(DeleteCommandProperty, value); }
    }
}
