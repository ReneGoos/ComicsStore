using System.Threading.Tasks;

namespace StoreFront.Navigation
{
    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }
}
