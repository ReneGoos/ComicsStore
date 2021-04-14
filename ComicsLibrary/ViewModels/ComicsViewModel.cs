using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using ComicsStore.MiddleWare.Common;
using ComicsLibrary.EditModels;

namespace ComicsLibrary.ViewModels
{
    public class ComicsViewModel : BasicViewModel
    {
        private readonly NavigationService _navigationService;

        public ICommand AddCharacterToStoryCommand { get; protected set; }
        public ICommand AddArtistToStoryCommand { get; protected set; }
        public ICommand AddBookToStoryCommand { get; protected set; }
        public ICommand AddCodeToStoryCommand { get; protected set; }
        public ICommand AddCodeToSeriesCommand { get; protected set; }
        public ICommand AddSeriesToBookCommand { get; protected set; }
        public ICommand AddPublisherToBookCommand { get; protected set; }

        private readonly IArtistsService _artistsService;
        private readonly IBooksService _booksService;
        private readonly ICharactersService _charactersService;
        private readonly ICodesService _codesService;
        private readonly IPublishersService _publishersService;
        private readonly ISeriesService _seriesService;
        private readonly IStoriesService _storiesService;
        private readonly IMapper _mapper;

        private List<string> FillEnum<T>() where T: Enum
        {
            return EnumHelper<T>.GetNames().ToList();
        }

        private async void AddArtistToStory()
        {
            var storyArtists = new List<StoryArtistEditModel>(StoryView.Item.StoryArtist.ToList().ConvertAll(s => new StoryArtistEditModel { ArtistId = s.ArtistId, StoryId = s.StoryId, ArtistType = s.ArtistType }));
            var result = await _navigationService.ShowDialogAsync(Windows.ArtistWindow);
            if (result ?? false)
            {
                StoryView.AddStoryArtist(storyArtists, ArtistView.Item.Id);
            };
        }

        private async void AddBookToStory()
        {
            var storyBooks = new List<StoryBookEditModel>(StoryView.Item.StoryBook.ToList().ConvertAll(s => new StoryBookEditModel { BookId = s.BookId, StoryId = s.StoryId }));
            var result = await _navigationService.ShowDialogAsync(Windows.BookWindow);
            if (result ?? false)
            {
                StoryView.AddStoryBook(storyBooks, BookView.Item.Id);
            };
        }

        private async void AddCharacterToStory()
        {
            var storyCharacters = new List<StoryCharacterEditModel>(StoryView.Item.StoryCharacter.ToList().ConvertAll(s => new StoryCharacterEditModel { CharacterId = s.CharacterId, StoryId = s.StoryId }));
            var result = await _navigationService.ShowDialogAsync(Windows.CharacterWindow);
            if (result ?? false)
            {
                StoryView.AddStoryCharacter(storyCharacters, CharacterView.Item.Id);
            };
        }

        private async void AddCodeToStory()
        {
            var result = await _navigationService.ShowDialogAsync(Windows.CodeWindow);
            if (result ?? false)
            {
                StoryView.AddStoryCode(CodeView.Item.Id);
            };
        }

        private async void AddCodeToSeries()
        {
            var result = await _navigationService.ShowDialogAsync(Windows.CodeWindow);
            if (result ?? false)
            {
                SeriesView.AddSeriesCode(CodeView.Item.Id);
            };
        }

        private async void AddSeriesToBook()
        {
            var bookSeries = new List<BookSeriesEditModel>(BookView.Item.BookSeries.ToList().ConvertAll(s => new BookSeriesEditModel { SeriesId = s.SeriesId, BookId = s.BookId, Issue = s.Issue, SeriesOrder = s.SeriesOrder }));
            var result = await _navigationService.ShowDialogAsync(Windows.SeriesWindow);
            if (result ?? false)
            {
                BookView.AddBookSeries(bookSeries, SeriesView.Item.Id);
            };
        }

        private async void AddPublisherToBook()
        {
            var storyPublishers = new List<BookPublisherEditModel>(BookView.Item.BookPublisher.ToList().ConvertAll(s => new BookPublisherEditModel { PublisherId = s.PublisherId, BookId = s.BookId }));
            var result = await _navigationService.ShowDialogAsync(Windows.PublisherWindow);
            if (result ?? false)
            {
                BookView.AddBookPublisher(storyPublishers, PublisherView.Item.Id);
            };
        }

        public ComicsViewModel(IArtistsService artistsService,
            IBooksService booksService,
            ICharactersService charactersService,
            ICodesService codesService,
            IPublishersService publishersService,
            ISeriesService seriesService,
            IStoriesService storiesService,
            IMapper mapper,
            NavigationService navigationService) : base(mapper)
        {
            _artistsService = artistsService;
            _booksService = booksService;
            _charactersService = charactersService;
            _codesService = codesService;
            _publishersService = publishersService;
            _seriesService = seriesService;
            _storiesService = storiesService;
            _mapper = mapper;
            _navigationService = navigationService;

            ArtistView = new ArtistViewModel(artistsService, mapper);
            BookView = new BookViewModel(booksService, mapper);
            CharacterView = new CharacterViewModel(charactersService, mapper);
            CodeView = new CodeViewModel(codesService, mapper);
            PublisherView = new PublisherViewModel(publishersService, mapper);
            SeriesView = new SeriesViewModel(seriesService, mapper);
            StoryView = new StoryViewModel(storiesService, mapper);

            Languages = LanguageType.FillLanguages();
            Actives = FillEnum<Active>();
            FirstPrints = FillEnum<FirstPrint>();
            BookTypes = FillEnum<BookType>();
            StoryTypes = FillEnum<StoryType>();

            AddArtistToStoryCommand = new RelayCommand(new Action(AddArtistToStory));
            AddBookToStoryCommand = new RelayCommand(new Action(AddBookToStory));
            AddCharacterToStoryCommand = new RelayCommand(new Action(AddCharacterToStory));
            AddCodeToStoryCommand = new RelayCommand(new Action(AddCodeToStory));
            AddCodeToSeriesCommand = new RelayCommand(new Action(AddCodeToSeries));
            AddSeriesToBookCommand = new RelayCommand(new Action(AddSeriesToBook));
            AddPublisherToBookCommand = new RelayCommand(new Action(AddPublisherToBook));
        }

    public ArtistViewModel ArtistView { get; private set; }
        public BookViewModel BookView { get; private set; }
        public CharacterViewModel CharacterView { get; private set; }
        public CodeViewModel CodeView { get; private set; }
        public PublisherViewModel PublisherView { get; private set; }
        public SeriesViewModel SeriesView { get; private set; }
        public StoryViewModel StoryView { get; private set; }

        public List<string> Actives { get; set; }
        public List<string> FirstPrints { get; set; }
        public List<string> BookTypes { get; set; }
        public List<string> StoryTypes { get; set; }
        public List<LanguageType> Languages { get; set; }

        public override bool IsDirty
        {
            get => base.IsDirty;
            set
            {
                base.IsDirty = value;
                ArtistView.IsDirty = value;
                BookView.IsDirty = value;
                CharacterView.IsDirty = value;
                CodeView.IsDirty = value;
                PublisherView.IsDirty = value;
                SeriesView.IsDirty = value;
                StoryView.IsDirty = value;
            }
        }
    }}
