using ComicsLibrary.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ComicsEntry
{
    public class ViewModelLocator
    {
        public ComicsViewModel ComicsViewModel => App.ServiceProvider.GetRequiredService<ComicsViewModel>();
    }
}
