using AutoMapper;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Output;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;

namespace ComicsStore.MiddleWare
{
    public class ComicsStoreProfile : Profile
    {
        public ComicsStoreProfile()
        {
            _ = CreateMap<string, Active>().ConstructUsing(src => EnumHelper<Active>.Parse(src));
            _ = CreateMap<string, BookType>().ConstructUsing(src => EnumHelper<BookType>.Parse(src));
            _ = CreateMap<string, StoryType>().ConstructUsing(src => EnumHelper<StoryType>.Parse(src));
            _ = CreateMap<ICollection<string>, ArtistType>().ConstructUsing(src => EnumHelper<ArtistType>.ParseFlags(src));
            _ = CreateMap<Active, string>().ConstructUsing(src => EnumHelper<Active>.GetDisplayValue(src));
            _ = CreateMap<BookType, string>().ConstructUsing(src => EnumHelper<BookType>.GetDisplayValue(src));
            _ = CreateMap<StoryType, string>().ConstructUsing(src => EnumHelper<StoryType>.GetDisplayValue(src));
            _ = CreateMap<ArtistType, ICollection<string>>().ConstructUsing(src => EnumHelper<ArtistType>.GetDisplayValues(src));

            _ = CreateMap<ArtistInputModel, Artist>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LastName))
                .ForSourceMember(src => src.Name, opt => opt.DoNotValidate());
            _ = CreateMap<BookInputModel, Book>();
            _ = CreateMap<CharacterInputModel, Character>();
            _ = CreateMap<CodeInputModel, Code>();
            _ = CreateMap<PublisherInputModel, Publisher>();
            _ = CreateMap<SeriesInputModel, Series>();
            _ = CreateMap<StoryInputModel, Story>();

            _ = CreateMap<BookInputPatchModel, Book>();
            _ = CreateMap<StoryInputPatchModel, Story>();

            _ = CreateMap<BookPublisherInputModel, BookPublisher>();
            _ = CreateMap<BookSeriesInputModel, BookSeries>();
            _ = CreateMap<PseudonymInputModel, Pseudonym>();
            _ = CreateMap<StoryArtistInputModel, StoryArtist>();
            _ = CreateMap<StoryBookInputModel, StoryBook>();
            _ = CreateMap<StoryCharacterInputModel, StoryCharacter>();

            _ = CreateMap<Artist, ArtistOutputModel>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));
            _ = CreateMap<Book, BookOutputModel>();
            _ = CreateMap<Character, CharacterOutputModel>();
            _ = CreateMap<Code, CodeOutputModel>();
            _ = CreateMap<Publisher, PublisherOutputModel>();
            _ = CreateMap<Series, SeriesOutputModel>();
            _ = CreateMap<Story, StoryOutputModel>();

            _ = CreateMap<Artist, ArtistOnlyOutputModel>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));
            _ = CreateMap<Book, BookOnlyOutputModel>();
            _ = CreateMap<Character, CharacterOnlyOutputModel>();
            _ = CreateMap<Code, CodeOnlyOutputModel>();
            _ = CreateMap<Publisher, PublisherOnlyOutputModel>();
            _ = CreateMap<Series, SeriesOnlyOutputModel>();
            _ = CreateMap<Story, StoryOnlyOutputModel>();

            _ = CreateMap<ExportBook, ExportBooksOutputModel>();
            _ = CreateMap<ExportStory, ExportBooksOutputModel>();

            _ = CreateMap<BookSeries, BookSeriesOutputModel>();
            _ = CreateMap<BookSeries, SeriesBookOutputModel>();
            _ = CreateMap<Series, BookSeriesOutputModel>();
            _ = CreateMap<Book, SeriesBookOutputModel>();

            _ = CreateMap<BookPublisher, BookPublisherOutputModel>();
            _ = CreateMap<BookPublisher, PublisherBookOutputModel>();
            _ = CreateMap<Publisher, BookPublisherOutputModel>();
            _ = CreateMap<Book, PublisherBookOutputModel>();

            _ = CreateMap<StoryArtist, ArtistStoryOutputModel>();
            _ = CreateMap<StoryArtist, StoryArtistOutputModel>();
            _ = CreateMap<Story, ArtistStoryOutputModel>();
            _ = CreateMap<Artist, StoryArtistOutputModel>();

            _ = CreateMap<Pseudonym, ArtistPseudonymOutputModel>();
            _ = CreateMap<Pseudonym, PseudonymArtistOutputModel>();

            _ = CreateMap<StoryBook, BookStoryOutputModel>();
            _ = CreateMap<StoryBook, StoryBookOutputModel>();
            _ = CreateMap<Story, BookStoryOutputModel>();
            _ = CreateMap<Book, StoryBookOutputModel>();

            _ = CreateMap<StoryCharacter, CharacterStoryOutputModel>();
            _ = CreateMap<StoryCharacter, StoryCharacterOutputModel>();
            _ = CreateMap<Character, StoryCharacterOutputModel>();
            _ = CreateMap<Story, CharacterStoryOutputModel>();

            _ = CreateMap<Series, CodeSeriesOutputModel>()
                .ForMember(cs => cs.SeriesId, opt => opt.MapFrom(s => s.Id))
                .ForMember(cs => cs.Series, opt => opt.MapFrom(s => s));
            _ = CreateMap<Story, CodeStoryOutputModel>()
                .ForMember(cs => cs.StoryId, opt => opt.MapFrom(s => s.Id))
                .ForMember(cs => cs.Story, opt => opt.MapFrom(s => s));
            _ = CreateMap<Story, StoryOriginOutputModel>()
                .ForMember(cs => cs.StoryId, opt => opt.MapFrom(s => s.Id))
                .ForMember(cs => cs.StoryFromOrigin, opt => opt.MapFrom(s => s));
        }
    }
}
