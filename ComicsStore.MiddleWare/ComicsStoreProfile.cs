using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using System;
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

            CreateMap<BookPublisherInputModel, BookPublisher>();
            CreateMap<BookSeriesInputModel, BookSeries>();
            CreateMap<StoryArtistInputModel, StoryArtist>();
            CreateMap<BasicBookInputModel, StoryBook>();
            CreateMap<BasicBookInputModel, BookPublisher>();
            CreateMap<BasicBookInputModel, BookSeries>();
            CreateMap<BookStoryInputModel, StoryBook>();
            CreateMap<StoryCharacterInputModel, StoryCharacter>();

            CreateMap<Artist, ArtistOutputModel>();
            CreateMap<Book, BookOutputModel>();
            CreateMap<Character, CharacterOutputModel>();
            CreateMap<Code, CodeOutputModel>();
            CreateMap<Publisher, PublisherOutputModel>();
            CreateMap<Series, SeriesOutputModel>();
            CreateMap<Story, StoryOutputModel>();
            CreateMap<ExportMemento, ExportMementoOutputModel>();
            CreateMap<ExportStory, ExportMementoOutputModel>();

            CreateMap<BookSeries, BookSeriesOutputModel>();
            CreateMap<BookPublisher, BookPublisherOutputModel>();
            CreateMap<BookSeries, SeriesBookOutputModel>();
            CreateMap<BookPublisher, BasicBookOutputModel>();

            CreateMap<StoryArtist, ArtistStoryOutputModel>();
            CreateMap<StoryBook, BasicStoryOutputModel>();
            CreateMap<StoryCharacter, BasicStoryOutputModel>();
            CreateMap<StoryArtist, StoryArtistOutputModel>();
            CreateMap<StoryBook, BasicBookOutputModel>();
            CreateMap<StoryCharacter, StoryCharacterOutputModel>();
        }
    }
}
