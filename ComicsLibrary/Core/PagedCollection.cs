using ComicsLibrary.EditModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Gaming.Input;

namespace ComicsLibrary.Core
{
    public class PagedCollection<TItem> : ObservableObject where TItem : BasicEditModel, ICrossEditModel
    {
        private int _selectedPage;
        private ObservableChangedCollection<TItem> _items;

        public ObservableChangedCollection<TItem> Items { get => _items; set { Set(ref _items, value); } }
        public int SelectedPage { get => _selectedPage; set { Set(ref _selectedPage, value); } }
        public int StepPage { get; set; }
        public bool FirstPage { get => _selectedPage == 0; }
        public bool LastPage
        {
            get
            {
                if (_items == null)
                    return true;
                return (_selectedPage + 1) >= Math.Ceiling((double)_items.Count / StepPage);
            }
        }

        public IEnumerable<TItem> SelectedItems { get => _items?.Skip(SelectedPage * StepPage).Take(StepPage); }

        public ICommand ButtonUpCommand { get; protected set; }
        public ICommand ButtonDownCommand { get; protected set; }

        public PagedCollection() : base()
        {
            SelectedPage = 0;
            StepPage = 10;

            ButtonDownCommand = new RelayCommand(new Action(ButtonDown));
            ButtonUpCommand = new RelayCommand(new Action(ButtonUp));

            PropertyChanged += PagedCollection_PropertyChanged;
        }

        private void PagedCollection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Items):
                case nameof(SelectedPage):
                    RaisePropertyChanged(nameof(SelectedItems));
                    RaisePropertyChanged(nameof(FirstPage));
                    RaisePropertyChanged(nameof(LastPage));

                    break;
            }
        }

        public void ButtonUp()
        {
            SelectedPage++;
        }

        public void ButtonDown()
        {
            SelectedPage--;
        }
    }
}
