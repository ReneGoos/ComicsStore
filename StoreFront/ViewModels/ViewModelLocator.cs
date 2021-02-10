using Microsoft.Extensions.DependencyInjection;

namespace StoreFront.ViewModels
{
    public class ViewModelLocator
    {
        public ArtistViewModel ArtistViewModel => App.ServiceProvider.GetRequiredService<ArtistViewModel>();
    }
}
