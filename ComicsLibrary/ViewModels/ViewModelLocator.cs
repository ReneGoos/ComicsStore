using Microsoft.Extensions.DependencyInjection;

namespace ComicsLibrary.ViewModels
{
    public class ViewModelLocator
    {
        public ArtistViewModel ArtistViewModel => App.ServiceProvider.GetRequiredService<ArtistViewModel>();
    }
}
