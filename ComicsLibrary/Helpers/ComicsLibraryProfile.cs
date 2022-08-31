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
            _ = CreateMap<ICollection<EnumCheckedType>, ICollection<string>>().ConstructUsing(src => CheckedArrayMapper<ArtistType>.GetStringList(src));
            _ = CreateMap<ICollection<string>, ICollection<EnumCheckedType>>().ConstructUsing(src => CheckedArrayMapper<ArtistType>.GetCheckedList(src));

            _ = CreateMap<ArtistEditModel, ArtistInputModel>();
            _ = CreateMap<BookEditModel, BookInputModel>();
            _ = CreateMap<CharacterEditModel, CharacterInputModel>();
            _ = CreateMap<CodeEditModel, CodeInputModel>();
            _ = CreateMap<PublisherEditModel, PublisherInputModel>();
            _ = CreateMap<SeriesEditModel, SeriesInputModel>();
            _ = CreateMap<StoryEditModel, StoryInputModel>();
            _ = CreateMap<ExportBooksOutputModel, ReportEditModel>();

            _ = CreateMap<ArtistOutputModel, ArtistEditModel>();
            _ = CreateMap<BookOutputModel, BookEditModel>();
            _ = CreateMap<CharacterOutputModel, CharacterEditModel>();
            _ = CreateMap<CodeOutputModel, CodeEditModel>();
            _ = CreateMap<PublisherOutputModel, PublisherEditModel>();
            _ = CreateMap<SeriesOutputModel, SeriesEditModel>();
            _ = CreateMap<StoryOutputModel, StoryEditModel>();

            _ = CreateMap<BookPublisherEditModel, BookPublisherInputModel>();
            _ = CreateMap<BookSeriesEditModel, BookSeriesInputModel>();

            _ = CreateMap<CodeStoryOutputModel, StoryCodeEditModel>();
            _ = CreateMap<CodeSeriesOutputModel, SeriesCodeEditModel>();

            _ = CreateMap<PseudonymEditModel, PseudonymInputModel>();

            _ = CreateMap<StoryArtistEditModel, StoryArtistInputModel>();
            _ = CreateMap<StoryBookEditModel, StoryBookInputModel>();
            _ = CreateMap<StoryCharacterEditModel, StoryCharacterInputModel>();

            _ = CreateMap<ArtistStoryOutputModel, StoryArtistEditModel>();
            _ = CreateMap<BookPublisherOutputModel, BookPublisherEditModel>();
            _ = CreateMap<BookSeriesOutputModel, BookSeriesEditModel>();
            _ = CreateMap<BookStoryOutputModel, StoryBookEditModel>();
            _ = CreateMap<CharacterStoryOutputModel, StoryCharacterEditModel>();
            _ = CreateMap<PseudonymOutputModel, PseudonymEditModel>();
            _ = CreateMap<PublisherBookOutputModel, BookPublisherEditModel>();
            _ = CreateMap<SeriesBookOutputModel, BookSeriesEditModel>();
            _ = CreateMap<StoryArtistOutputModel, StoryArtistEditModel>();
            _ = CreateMap<StoryBookOutputModel, StoryBookEditModel>();
            _ = CreateMap<StoryCharacterOutputModel, StoryCharacterEditModel>();
        }
    }
}
