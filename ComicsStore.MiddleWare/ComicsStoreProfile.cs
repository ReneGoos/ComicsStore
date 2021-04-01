using AutoMapper;
using ComicsStore.Data.Model;
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
            CreateMap<string, Active>().ConstructUsing(src => EnumHelper<Active>.Parse(src));
            CreateMap<string, BookType>().ConstructUsing(src => EnumHelper<BookType>.Parse(src));
            CreateMap<string, StoryType>().ConstructUsing(src => EnumHelper<StoryType>.Parse(src));
            CreateMap<ICollection<string>, ArtistType>().ConstructUsing(src => EnumHelper<ArtistType>.ParseFlags(src));
            CreateMap<Active, string>().ConstructUsing(src => EnumHelper<Active>.GetDisplayValue(src));
            CreateMap<BookType, string>().ConstructUsing(src => EnumHelper<BookType>.GetDisplayValue(src));
            CreateMap<StoryType, string>().ConstructUsing(src => EnumHelper<StoryType>.GetDisplayValue(src));
            CreateMap<ArtistType, ICollection<string>>().ConstructUsing(src => (ICollection<string>)EnumHelper<ArtistType>.GetDisplayValues(src));

            CreateMap<ArtistInputModel, Artist>();
            CreateMap<BookInputModel, Book>();
            CreateMap<CharacterInputModel, Character>();
            CreateMap<CodeInputModel, Code>();
            CreateMap<PublisherInputModel, Publisher>();
            CreateMap<SeriesInputModel, Series>();
            CreateMap<StoryInputModel, Story>();

            CreateMap<BookInputPatchModel, Book>();
            CreateMap<StoryInputPatchModel, Story>();

            CreateMap<BookPublisherInputModel, BookPublisher>();
            CreateMap<BookSeriesInputModel, BookSeries>();
            CreateMap<StoryArtistInputModel, StoryArtist>();
            CreateMap<StoryBookInputModel, StoryBook>();
            CreateMap<StoryBookInputModel, BookPublisher>();
            CreateMap<StoryBookInputModel, BookSeries>();
            CreateMap<BookStoryInputModel, StoryBook>();
            CreateMap<StoryCharacterInputModel, StoryCharacter>();

            CreateMap<Artist, ArtistOutputModel>();
            CreateMap<Book, BookOutputModel>();
            CreateMap<Character, CharacterOutputModel>();
            CreateMap<Code, CodeOutputModel>();
            CreateMap<Publisher, PublisherOutputModel>();
            CreateMap<Series, SeriesOutputModel>();
            CreateMap<Story, StoryOutputModel>();

            CreateMap<Artist, ArtistOnlyOutputModel>();
            CreateMap<Book, BookOnlyOutputModel>();
            CreateMap<Character, CharacterOnlyOutputModel>();
            CreateMap<Code, CodeOnlyOutputModel>();
            CreateMap<Publisher, PublisherOnlyOutputModel>();
            CreateMap<Series, SeriesOnlyOutputModel>(); 
            CreateMap<Story, StoryOnlyOutputModel>();

            CreateMap<ExportBook, ExportBooksOutputModel>();
            CreateMap<ExportStory, ExportBooksOutputModel>();

            CreateMap<BookSeries, BookSeriesOutputModel>().IncludeMembers(b => b.Series);
            CreateMap<BookSeries, SeriesBookOutputModel>().IncludeMembers(b => b.Book);
            CreateMap<Series, BookSeriesOutputModel>();
            CreateMap<Book, SeriesBookOutputModel>();

            CreateMap<BookPublisher, BookPublisherOutputModel>().IncludeMembers(b => b.Publisher);
            CreateMap<BookPublisher, PublisherBookOutputModel>().IncludeMembers(b => b.Book);
            CreateMap<Publisher, BookPublisherOutputModel>();
            CreateMap<Book, PublisherBookOutputModel>();

            CreateMap<StoryArtist, ArtistStoryOutputModel>().IncludeMembers(s => s.Story);
            CreateMap<StoryArtist, StoryArtistOutputModel>().IncludeMembers(s => s.Artist);
            CreateMap<Story, ArtistStoryOutputModel>();
            CreateMap<Artist, StoryArtistOutputModel>();

            CreateMap<StoryBook, BookStoryOutputModel>().IncludeMembers(s => s.Story);
            CreateMap<StoryBook, StoryBookOutputModel>().IncludeMembers(s => s.Book);
            CreateMap<Story, BookStoryOutputModel>();
            CreateMap<Book, StoryBookOutputModel>();

            CreateMap<StoryCharacter, CharacterStoryOutputModel>().IncludeMembers(s => s.Story);
            CreateMap<StoryCharacter, StoryCharacterOutputModel>().IncludeMembers(s => s.Character);
            CreateMap<Character, StoryCharacterOutputModel>();
            CreateMap<Story, CharacterStoryOutputModel>();

            CreateMap<Series, CodeSeriesOutputModel>()
                .ForMember(cs => cs.SeriesId, opt => opt.MapFrom(s => s.Id));
            CreateMap<Story, CodeStoryOutputModel>()
                .ForMember(cs => cs.StoryId, opt => opt.MapFrom(s => s.Id));
        }
    }
}
