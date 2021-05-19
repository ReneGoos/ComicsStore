using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.ViewModels.Interfaces;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels
{
    public class BasicViewModel : ObservableObject, IBasicViewModel
    {
        private ICommand _getCommand;
        private ICommand _newCommand;
        private ICommand _saveCommand;
        private ICommand _undoCommand;
        private ICommand _deleteCommand;

        public ICommand GetCommand { get => _getCommand; protected set => _getCommand = value; }
        public ICommand NewCommand { get => _newCommand; protected set => _newCommand = value; }
        public ICommand SaveCommand { get => _saveCommand; protected set => _saveCommand = value; }
        public ICommand UndoCommand { get => _undoCommand; protected set => _undoCommand = value; }
        public ICommand DeleteCommand { get => _deleteCommand; protected set => _deleteCommand = value; }

        public IMapper Mapper { get; private set; }

        public BasicViewModel(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
