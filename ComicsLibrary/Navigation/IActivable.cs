namespace ComicsLibrary.Navigation;

public interface IActivable
{
    Task ActivateAsync(object parameter);
}
