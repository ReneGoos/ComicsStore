using Microsoft.Extensions.DependencyInjection;

namespace ComicsLibrary.ViewModels
{
    public class ViewModelLocator
    {
        public ComicsViewModel ComicsViewModel => App.ServiceProvider.GetRequiredService<ComicsViewModel>();

        public ArtistViewModel ArtistViewModel => App.ServiceProvider.GetRequiredService<ArtistViewModel>();
        public StoryViewModel StoryViewModel => App.ServiceProvider.GetRequiredService<StoryViewModel>();
    }
}
