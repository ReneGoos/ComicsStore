using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.Navigation;
using ComicsLibrary.ViewModels.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels
{
    public abstract class BasicViewModel : ObservableObject, IBasicViewModel, INotifyItemChanged
    {
        private ICommand _getCommand;
        private ICommand _newCommand;
        private ICommand _saveCommand;
        private ICommand _undoCommand;
        private ICommand _deleteCommand;
        private ICommand _exitCommand;

        public event ItemChangedEventHandler ItemChanged;

        protected void RaiseItemChanged(string itemName, int? id, ActionType actionType)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(itemName, id, actionType));
        }

        public ICommand GetCommand { get => _getCommand; protected set => _getCommand = value; }
        public ICommand NewCommand { get => _newCommand; protected set => _newCommand = value; }
        public ICommand SaveCommand { get => _saveCommand; protected set => _saveCommand = value; }
        public ICommand UndoCommand { get => _undoCommand; protected set => _undoCommand = value; }
        public ICommand DeleteCommand { get => _deleteCommand; protected set => _deleteCommand = value; }
        public ICommand ExitCommand { get => _exitCommand; protected set => _exitCommand = value; }

        public IMapper Mapper { get; }
        public INavigationService NavigationService { get; }

        public BasicViewModel(INavigationService navigationService,
            IMapper mapper)
        {
            Mapper = mapper;
            NavigationService = navigationService;
        }

        //public abstract TableEditModel Item { get; }

        public void GetItem(int? itemId)
        {
            if (itemId.HasValue) // && (!itemView.Item.Id.HasValue || itemId.Value != itemView.Item.Id.Value))
            {
                GetCommand.Execute(itemId);
            }
            //else if (Item.Id.HasValue)
            //{
            //    GetCommand.Execute(Item.Id);
            //}
        }
    }
}
