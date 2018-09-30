using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;

namespace ComicsStore.MiddleWare
{
    public class ComicsStoreProfile : Profile
    {
        public ComicsStoreProfile()
        {
            CreateMap<ArtistInputModel, Artist>();
            CreateMap<BookInputModel, Book>();
            CreateMap<CharacterInputModel, Character>();
            CreateMap<CodeInputModel, Code>();
            CreateMap<PublisherInputModel, Publisher>();
            CreateMap<SeriesInputModel, Series>();
            CreateMap<StoryInputModel, Story>();

            CreateMap<Artist, ArtistOutputModel>();
            CreateMap<Book, BookOutputModel>();
            CreateMap<Character, CharacterOutputModel>();
            CreateMap<Code, CodeOutputModel>();
            CreateMap<Publisher, PublisherOutputModel>();
            CreateMap<Series, SeriesOutputModel>();
            CreateMap<Story, StoryOutputModel>();

            CreateMap<BookPublisher, PublisherOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Publisher.Id))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.PublisherName))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Publisher.Remark));
            CreateMap<BookSeries, SeriesBookOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Book.Id))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.BookName))
                .ForMember(dest => dest.BookType, opt => opt.MapFrom(src => src.Book.BookType))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Book.Remark));
            CreateMap<BookSeries, BookSeriesOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Series.Id))
                .ForMember(dest => dest.SeriesName, opt => opt.MapFrom(src => src.Series.SeriesName))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Series.Remark));
            CreateMap<StoryArtist, StoryArtistOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Artist.Id))
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.ArtistName))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Artist.Remark));
            CreateMap<StoryArtist, ArtistStoryOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Story.Id))
                .ForMember(dest => dest.StoryName, opt => opt.MapFrom(src => src.Story.StoryName))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Story.Remark));
            CreateMap<StoryBook, BookOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Book.Id))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.BookName))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Book.Remark));
            CreateMap<StoryCharacter, CharacterOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Character.Id))
                .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Character.CharacterName))
                .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Character.Remark));
        }
    }
}
