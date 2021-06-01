using ComicsLibrary.EditModels;
using ComicsStore.Data.Common;
using ComicsStore.MiddleWare;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;

namespace ComicsLibrary.Helpers
{
    public class ComicsLibraryProfile : ComicsStoreProfile
    {
        public ComicsLibraryProfile() : base()
        {
            CreateMap<ICollection<RoleType>, ICollection<string>>().ConstructUsing(src => (ICollection<string>)CheckedArrayMapper<ArtistType>.GetStringList(src));
            CreateMap<ICollection<string>, ICollection<RoleType>>().ConstructUsing(src => (ICollection<RoleType>)CheckedArrayMapper<ArtistType>.GetCheckedList(src));

            CreateMap<ArtistEditModel, ArtistInputModel>();
            CreateMap<BookEditModel, BookInputModel>();
            CreateMap<CharacterEditModel, CharacterInputModel>();
            CreateMap<CodeEditModel, CodeInputModel>();
            CreateMap<PublisherEditModel, PublisherInputModel>();
            CreateMap<SeriesEditModel, SeriesInputModel>();
            CreateMap<StoryEditModel, StoryInputModel>();
            CreateMap<ExportBooksOutputModel, ReportEditModel>();

            CreateMap<ArtistOutputModel, ArtistEditModel>();
            CreateMap<BookOutputModel, BookEditModel>();
            CreateMap<CharacterOutputModel, CharacterEditModel>();
            CreateMap<CodeOutputModel, CodeEditModel>();
            CreateMap<PublisherOutputModel, PublisherEditModel>();
            CreateMap<SeriesOutputModel, SeriesEditModel>();
            CreateMap<StoryOutputModel, StoryEditModel>();

            CreateMap<BookPublisherEditModel, BookPublisherInputModel>();
            CreateMap<BookSeriesEditModel, BookSeriesInputModel>();

            CreateMap<CodeStoryOutputModel, StoryCodeEditModel>();
            CreateMap<CodeSeriesOutputModel, SeriesCodeEditModel>();

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
