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
            _ = CreateMap<ICollection<EnumCheckedType>, ICollection<string>>()
                .ConstructUsing(src => CheckedArrayMapper<ArtistType>.GetStringList(src));
            _ = CreateMap<ICollection<string>, ICollection<EnumCheckedType>>()
                .ConstructUsing(src => CheckedArrayMapper<ArtistType>.GetCheckedList(src));

            _ = CreateMap<ArtistEditModel, ArtistInputModel>();
            _ = CreateMap<BookEditModel, BookInputModel>();
            _ = CreateMap<CharacterEditModel, CharacterInputModel>();
            _ = CreateMap<CodeEditModel, CodeInputModel>();
            _ = CreateMap<PublisherEditModel, PublisherInputModel>();
            _ = CreateMap<SeriesEditModel, SeriesInputModel>()
                .ForMember(src => src.Code, opt => opt.Ignore());
            _ = CreateMap<StoryEditModel, StoryInputModel>()
                .ForMember(src => src.Code, opt => opt.Ignore())
                .ForMember(src => src.OriginStory, opt => opt.Ignore());
            _ = CreateMap<ExportBooksOutputModel, ReportEditModel>();

            _ = CreateMap<ArtistOutputModel, ArtistEditModel>();
            _ = CreateMap<BookOutputModel, BookEditModel>();
            _ = CreateMap<CharacterOutputModel, CharacterEditModel>();
            _ = CreateMap<CodeOutputModel, CodeEditModel>();
            _ = CreateMap<PublisherOutputModel, PublisherEditModel>();
            _ = CreateMap<SeriesOutputModel, SeriesEditModel>();
            _ = CreateMap<StoryOutputModel, StoryEditModel>();

            _ = CreateMap<ArtistOnlyOutputModel, ArtistOnlyEditModel>();
            _ = CreateMap<BookOnlyOutputModel, BookOnlyEditModel>();
            _ = CreateMap<CharacterOnlyOutputModel, CharacterOnlyEditModel>();
            _ = CreateMap<CodeOnlyOutputModel, CodeOnlyEditModel>();
            _ = CreateMap<PublisherOnlyOutputModel, PublisherOnlyEditModel>();
            _ = CreateMap<SeriesOnlyOutputModel, SeriesOnlyEditModel>();
            _ = CreateMap<StoryOnlyOutputModel, StoryOnlyEditModel>();

            _ = CreateMap<ArtistPseudonymEditModel, PseudonymInputModel>();
            _ = CreateMap<ArtistStoryEditModel, StoryArtistInputModel>();

            _ = CreateMap<BookPublisherEditModel, BookPublisherInputModel>();
            _ = CreateMap<BookSeriesEditModel, BookSeriesInputModel>();
            _ = CreateMap<BookStoryEditModel, StoryBookInputModel>();

            _ = CreateMap<CharacterStoryEditModel, StoryCharacterInputModel>();

            _ = CreateMap<PseudonymArtistEditModel, PseudonymInputModel>();

            _ = CreateMap<PublisherBookEditModel, BookPublisherInputModel>();
            _ = CreateMap<SeriesBookEditModel, BookSeriesInputModel>();

            _ = CreateMap<StoryArtistEditModel, StoryArtistInputModel>();
            _ = CreateMap<StoryBookEditModel, StoryBookInputModel>();
            _ = CreateMap<StoryCharacterEditModel, StoryCharacterInputModel>();

            _ = CreateMap<ArtistStoryOutputModel, ArtistStoryEditModel>();
            _ = CreateMap<ArtistPseudonymOutputModel, ArtistPseudonymEditModel>();
            _ = CreateMap<BookPublisherOutputModel, BookPublisherEditModel>();
            _ = CreateMap<BookSeriesOutputModel, BookSeriesEditModel>();
            _ = CreateMap<BookStoryOutputModel, BookStoryEditModel>();
            _ = CreateMap<CharacterStoryOutputModel, CharacterStoryEditModel>();
            _ = CreateMap<CodeStoryOutputModel, CodeStoryEditModel>();
            _ = CreateMap<CodeSeriesOutputModel, CodeSeriesEditModel>();
            _ = CreateMap<PseudonymArtistOutputModel, PseudonymArtistEditModel>();
            _ = CreateMap<PublisherBookOutputModel, PublisherBookEditModel>();
            _ = CreateMap<SeriesBookOutputModel, SeriesBookEditModel>();
            _ = CreateMap<StoryArtistOutputModel, StoryArtistEditModel>();
            _ = CreateMap<StoryBookOutputModel, StoryBookEditModel>();
            _ = CreateMap<StoryCharacterOutputModel, StoryCharacterEditModel>();
            _ = CreateMap<StoryOriginOutputModel, StoryOriginEditModel>();
        }
    }
}
