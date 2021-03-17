using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;

namespace ComicsLibrary.Helpers
{
    public class ComicsLibraryProfile : ComicsStoreProfile
    {
        public ComicsLibraryProfile() : base()
        {
            CreateMap<ArtistEditModel, ArtistInputModel>();
            CreateMap<BookEditModel, BookInputModel>();
            CreateMap<CharacterEditModel, CharacterInputModel>();
            CreateMap<CodeEditModel, CodeInputModel>();
            CreateMap<PublisherEditModel, PublisherInputModel>();
            CreateMap<SeriesEditModel, SeriesInputModel>();
            CreateMap<StoryEditModel, StoryInputModel>();

            CreateMap<ArtistOutputModel, ArtistEditModel>();
            CreateMap<BookOutputModel, BookEditModel>();
            CreateMap<CharacterOutputModel, CharacterEditModel>();
            CreateMap<CodeOutputModel, CodeEditModel>();
            CreateMap<PublisherOutputModel, PublisherEditModel>();
            CreateMap<SeriesOutputModel, SeriesEditModel>();
            CreateMap<StoryOutputModel, StoryEditModel>();

            //CreateMap<StoryArtistEditModel, ArtistStoryInputModel>();
            CreateMap<BookPublisherEditModel, BookPublisherInputModel>();
            CreateMap<BookSeriesEditModel, BookSeriesInputModel>();
            CreateMap<StoryBookEditModel, BookStoryInputModel>();
            //CreateMap<StoryCharacterEditModel, CharacterStoryInputModel>();
            //CreateMap<BookPublisherEditModel, PublisherBookInputModel>();
            //CreateMap<BookSeriesEditModel, SeriesBookInputModel>();
            CreateMap<StoryArtistEditModel, StoryArtistInputModel>();
            CreateMap<StoryBookEditModel, StoryBookInputModel>();
            CreateMap<StoryCharacterEditModel, StoryCharacterInputModel>();

            CreateMap<ArtistStoryOutputModel, StoryArtistEditModel>();
            CreateMap<BookPublisherOutputModel, BookPublisherEditModel>();
            CreateMap<BookSeriesOutputModel, BookSeriesEditModel>();
            CreateMap<BookStoryOutputModel, StoryBookEditModel>();
            CreateMap<CharacterStoryOutputModel, StoryCharacterEditModel>();
            CreateMap<PublisherBookOutputModel, BookPublisherEditModel>();
            CreateMap<SeriesBookOutputModel, BookSeriesEditModel>();
            CreateMap<StoryArtistOutputModel, StoryArtistEditModel>();
            CreateMap<StoryBookOutputModel, StoryBookEditModel>();
            CreateMap<StoryCharacterOutputModel, StoryCharacterEditModel>();
        }
    }
}
