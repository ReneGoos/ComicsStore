using AutoMapper;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels.Interfaces
{
    public interface IBasicViewModel
    {
        ICommand UndoCommand { get; }
        ICommand GetCommand { get; }
        IMapper Mapper { get; }
        ICommand NewCommand { get; }
        ICommand SaveCommand { get; }
    }
}