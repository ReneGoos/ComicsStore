using AutoMapper;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComicsLibrary.ViewModels
{
    public abstract class InputViewModel : INotifyPropertyChanged
    {
        protected readonly IMapper mapper;

        public InputViewModel(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
