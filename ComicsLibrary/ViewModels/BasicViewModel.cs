using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels
{
    public class BasicViewModel : INotifyPropertyChanged
    {
        private bool _isDirty;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public virtual bool IsDirty { get => _isDirty; set { _isDirty = value; RaisePropertyChanged(); RaisePropertyChanged("IsClean"); } }
        public bool IsClean { get => !IsDirty; }
    }
}
