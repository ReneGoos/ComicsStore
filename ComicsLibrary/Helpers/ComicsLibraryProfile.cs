using ComicsStore.MiddleWare;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.Models;
using System.Collections.Generic;

namespace ComicsLibrary.Helpers
{
    public class ComicsLibraryProfile : ComicsStoreProfile
    {
        public ComicsLibraryProfile() : base()
        {
            /*
            CreateMap<string, Active>().ConstructUsing(src => EnumHelper<Active>.Parse(src));
            CreateMap<string, BookType>().ConstructUsing(src => EnumHelper<BookType>.Parse(src));
            CreateMap<string, StoryType>().ConstructUsing(src => EnumHelper<StoryType>.Parse(src));
            CreateMap<List<string>, ArtistType>().ConstructUsing(src => EnumHelper<ArtistType>.ParseFlags(src));
            CreateMap<Active, string>().ConstructUsing(src => EnumHelper<Active>.GetDisplayValue(src));
            CreateMap<BookType, string>().ConstructUsing(src => EnumHelper<BookType>.GetDisplayValue(src));
            CreateMap<StoryType, string>().ConstructUsing(src => EnumHelper<StoryType>.GetDisplayValue(src));
            CreateMap<ArtistType, List<string>>().ConstructUsing(src => (List<string>)EnumHelper<ArtistType>.GetDisplayValues(src));

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
            CreateMap<BookPublisher, BookOnlyOutputModel>().IncludeMembers(b => b.Book);
            CreateMap<Publisher, BookPublisherOutputModel>();

            CreateMap<StoryArtist, ArtistStoryOutputModel>().IncludeMembers(s => s.Story);
            CreateMap<StoryArtist, StoryArtistOutputModel>().IncludeMembers(s => s.Artist);
            CreateMap<Story, ArtistStoryOutputModel>().IncludeMembers();
            CreateMap<Artist, StoryArtistOutputModel>().IncludeMembers();

            CreateMap<StoryBook, StoryOnlyOutputModel>().IncludeMembers(s => s.Story);
            CreateMap<StoryBook, BookOnlyOutputModel>().IncludeMembers(s => s.Book);

            CreateMap<StoryCharacter, StoryOnlyOutputModel>().IncludeMembers(s => s.Story);
            CreateMap<StoryCharacter, StoryCharacterOutputModel>().IncludeMembers(s => s.Character);
            CreateMap<Character, StoryCharacterOutputModel>();
            */

            CreateMap<ArtistOutputModel, ArtistModel>();
            CreateMap<ArtistStoryOutputModel, ArtistStoryModel>();
        }
    }
}
