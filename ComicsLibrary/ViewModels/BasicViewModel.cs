using AutoMapper;
using ComicsLibrary.Core;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels
{
    public class BasicViewModel : ObservableObject 
    {
        private ICommand _getCommand;
        private ICommand _newCommand;
        private ICommand _saveCommand;
        private ICommand _cancelSaveCommand;

        public ICommand GetCommand { get => _getCommand; protected set => _getCommand = value; }
        public ICommand NewCommand { get => _newCommand; protected set => _newCommand = value; }
        public ICommand SaveCommand { get => _saveCommand; protected set => _saveCommand = value; }
        public ICommand CancelSaveCommand { get => _cancelSaveCommand; protected set => _cancelSaveCommand = value; }

        public IMapper Mapper { get; private set; }

        public BasicViewModel(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
