using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using ComicsStore.MiddleWare.Common;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.ViewModels.Interfaces;
using ComicsStore.Data.Common;
using Microsoft.Extensions.Configuration;

namespace ComicsLibrary.ViewModels
{
    public class ComicsViewModel : BasicViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly IConfiguration _configuration;

        public ICommand ShowArtistFromStoryWindowCommand { get; protected set; }
        public ICommand ShowBookFromPublisherWindowCommand { get; protected set; }
        public ICommand ShowBookFromSeriesWindowCommand { get; protected set; }
        public ICommand ShowBookFromStoryWindowCommand { get; protected set; }
        public ICommand ShowCharacterFromStoryWindowCommand { get; protected set; }
        public ICommand ShowCodeFromSeriesWindowCommand { get; protected set; }
        public ICommand ShowCodeFromStoryWindowCommand { get; protected set; }
        public ICommand ShowPublisherFromBookWindowCommand { get; protected set; }
        public ICommand ShowSeriesFromBookWindowCommand { get; protected set; }
        public ICommand ShowSeriesFromCodeWindowCommand { get; protected set; }
        public ICommand ShowStoryFromArtistWindowCommand { get; protected set; }
        public ICommand ShowStoryFromBookWindowCommand { get; protected set; }
        public ICommand ShowStoryFromCharacterWindowCommand { get; protected set; }
        public ICommand ShowStoryFromCodeWindowCommand { get; protected set; }

        public ICommand ShowArtistWindowCommand { get; protected set; }
        public ICommand ShowBookWindowCommand { get; protected set; }
        public ICommand ShowCharacterWindowCommand { get; protected set; }
        public ICommand ShowCodeWindowCommand { get; protected set; }
        public ICommand ShowPublisherWindowCommand { get; protected set; }
        public ICommand ShowSeriesWindowCommand { get; protected set; }
        public ICommand ShowStoryWindowCommand { get; protected set; }
        public ICommand ShowReportWindowCommand { get; protected set; }

        private static List<string> FillEnum<T>() where T : Enum
        {
            return EnumHelper<T>.GetNames().ToList();
        }

        private static void GetItem<TEdit, TOut>(IBasicTableViewModel<TEdit, TOut> itemView, int? itemId)
            where TEdit : TableEditModel, new()
            where TOut : BasicOutputModel
        {
            if (itemId.HasValue) // && (!itemView.Item.Id.HasValue || itemId.Value != itemView.Item.Id.Value))
            {
                itemView.GetCommand.Execute(itemId);
            }
            else if (itemView.Item.Id.HasValue)
            {
                itemView.GetCommand.Execute(itemView.Item.Id);
            }
        }

        private async void ShowReportWindow()
        {
            await _navigationService.ShowAsync(StoreWindows.ReportWindow);
        }

        private async void ShowArtistWindow()
        {
            _ = await _navigationService.ShowDialogAsync(StoreWindows.ArtistWindow);
        }

        private async void ShowBookWindow()
        {
            _ = await _navigationService.ShowDialogAsync(StoreWindows.BookWindow);
        }

        private async void ShowCharacterWindow()
        {
            _ = await _navigationService.ShowDialogAsync(StoreWindows.CharacterWindow);
        }

        private async void ShowCodeWindow()
        {
            _ = await _navigationService.ShowDialogAsync(StoreWindows.CodeWindow);
        }

        private async void ShowPublisherWindow()
        {
            _ = await _navigationService.ShowDialogAsync(StoreWindows.PublisherWindow);
        }

        private async void ShowSeriesWindow()
        {
            _ = await _navigationService.ShowDialogAsync(StoreWindows.SeriesWindow);
        }

        private async void ShowStoriesWindow()
        {
            _ = await _navigationService.ShowDialogAsync(StoreWindows.StoryWindow);
        }

        private async void ShowArtistFromStoryWindow(int? itemId)
        {
            GetItem(ArtistView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.ArtistWindow);
            StoryView.AddStoryArtist(ArtistView.Item.Id ?? itemId);
        }

        private async void ShowBookFromPublisherWindow(int? itemId)
        {
            GetItem(BookView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.BookWindow);
            PublisherView.AddBookPublisher(BookView.Item.Id ?? itemId);
        }

        private async void ShowBookFromSeriesWindow(int? itemId)
        {
            GetItem(BookView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.BookWindow);
            SeriesView.AddBookSeries(BookView.Item.Id ?? itemId);
        }

        private async void ShowBookFromStoryWindow(int? itemId)
        {
            GetItem(BookView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.BookWindow);
            StoryView.AddStoryBook(BookView.Item.Id ?? itemId);
        }

        private async void ShowCharacterFromStoryWindow(int? itemId)
        {
            GetItem(CharacterView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.CharacterWindow);
            StoryView.AddStoryCharacter(CharacterView.Item.Id ?? itemId);
        }

        private async void ShowCodeFromSeriesWindow(int? itemId)
        {
            GetItem(CodeView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.CodeWindow);
            SeriesView.AddSeriesCode(CodeView.Item.Id ?? itemId);
        }

        private async void ShowCodeFromStoryWindow(int? itemId)
        {
            GetItem(CodeView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.CodeWindow);
            StoryView.AddStoryCode(CodeView.Item.Id ?? itemId);
        }

        private async void ShowPublisherFromBookWindow(int? itemId)
        {
            GetItem(CharacterView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.PublisherWindow);
            BookView.AddBookPublisher(PublisherView.Item.Id ?? itemId);
        }

        private async void ShowSeriesFromBookWindow(int? itemId)
        {
            GetItem(SeriesView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.SeriesWindow);
            BookView.AddBookSeries(SeriesView.Item.Id ?? itemId);
        }

        private async void ShowSeriesFromCodeWindow(int? itemId)
        {
            GetItem(SeriesView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.SeriesWindow);
            GetItem(CodeView, CodeView.Item.Id ?? itemId);
        }

        private async void ShowStoryFromArtistWindow(int? itemId)
        {
            GetItem(StoryView, itemId);
            _ = await _navigationService.ShowDialogAsync(StoreWindows.StoryWindow);
            ArtistView.AddArtistStory(StoryView.Item.Id ?? itemId);
        }

        private async void ShowStoryFromBookWindow(int? itemId)
        {
            GetItem(StoryView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.StoryWindow);
            BookView.AddStoryBook(StoryView.Item.Id ?? itemId);
        }

        private async void ShowStoryFromCharacterWindow(int? itemId)
        {
            GetItem(StoryView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.StoryWindow);
            CharacterView.AddStoryCharacter(StoryView.Item.Id ?? itemId);
        }

        private async void ShowStoryFromCodeWindow(int? itemId)
        {
            GetItem(StoryView, itemId);

            _ = await _navigationService.ShowDialogAsync(StoreWindows.StoryWindow);
            GetItem(CodeView, CodeView.Item.Id ?? itemId);
        }

        public ComicsViewModel(IArtistsService artistsService,
            IBooksService booksService,
            ICharactersService charactersService,
            ICodesService codesService,
            IPublishersService publishersService,
            ISeriesService seriesService,
            IStoriesService storiesService,
            IExportBooksService exportBooksService,
            IMapper mapper,
            NavigationService navigationService,
            IConfiguration configuration) : base(mapper)
        {
            _navigationService = navigationService;
            _configuration = configuration;
            ArtistView = new ArtistViewModel(artistsService, mapper);
            BookView = new BookViewModel(booksService, mapper);
            CharacterView = new CharacterViewModel(charactersService, mapper);
            CodeView = new CodeViewModel(codesService, mapper);
            PublisherView = new PublisherViewModel(publishersService, mapper);
            SeriesView = new SeriesViewModel(seriesService, mapper);
            StoryView = new StoryViewModel(storiesService, mapper);
            ReportView = new ReportViewModel(exportBooksService, mapper);

            Languages = LanguageType.FillLanguages();
            Actives = FillEnum<Active>();
            FirstPrints = FillEnum<FirstPrint>();
            BookTypes = FillEnum<BookType>();
            StoryTypes = FillEnum<StoryType>();

            ShowArtistFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowArtistFromStoryWindow));
            ShowBookFromPublisherWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowBookFromPublisherWindow));
            ShowBookFromSeriesWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowBookFromSeriesWindow));
            ShowBookFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowBookFromStoryWindow));
            ShowCharacterFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCharacterFromStoryWindow));
            ShowCodeFromSeriesWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCodeFromSeriesWindow));
            ShowCodeFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCodeFromStoryWindow));
            ShowPublisherFromBookWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowPublisherFromBookWindow));
            ShowSeriesFromBookWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowSeriesFromBookWindow));
            ShowSeriesFromCodeWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowSeriesFromCodeWindow));
            ShowStoryFromArtistWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromArtistWindow));
            ShowStoryFromBookWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromBookWindow));
            ShowStoryFromCharacterWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromCharacterWindow));
            ShowStoryFromCodeWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromCodeWindow));

            ShowArtistWindowCommand = new RelayCommand(new Action(ShowArtistWindow));
            ShowBookWindowCommand = new RelayCommand(new Action(ShowBookWindow));
            ShowCharacterWindowCommand = new RelayCommand(new Action(ShowCharacterWindow));
            ShowCodeWindowCommand = new RelayCommand(new Action(ShowCodeWindow));
            ShowPublisherWindowCommand = new RelayCommand(new Action(ShowPublisherWindow));
            ShowSeriesWindowCommand = new RelayCommand(new Action(ShowSeriesWindow));
            ShowStoryWindowCommand = new RelayCommand(new Action(ShowStoriesWindow));
            ShowReportWindowCommand = new RelayCommand(new Action(ShowReportWindow));
        }

        public ArtistViewModel ArtistView { get; private set; }
        public BookViewModel BookView { get; private set; }
        public CharacterViewModel CharacterView { get; private set; }
        public CodeViewModel CodeView { get; private set; }
        public PublisherViewModel PublisherView { get; private set; }
        public SeriesViewModel SeriesView { get; private set; }
        public StoryViewModel StoryView { get; private set; }
        public ReportViewModel ReportView { get; private set; }

        public List<string> Actives { get; set; }
        public List<string> FirstPrints { get; set; }
        public List<string> BookTypes { get; set; }
        public List<string> StoryTypes { get; set; }
        public List<LanguageType> Languages { get; set; }

        public bool OpenArtist => _navigationService.DialogActive(StoreWindows.ArtistWindow);
        public bool OpenBook => _navigationService.DialogActive(StoreWindows.BookWindow);
        public bool OpenCharacter => _navigationService.DialogActive(StoreWindows.CharacterWindow);
        public bool OpenCode => _navigationService.DialogActive(StoreWindows.CodeWindow);
        public bool OpenPublisher => _navigationService.DialogActive(StoreWindows.PublisherWindow);
        public bool OpenSeries => _navigationService.DialogActive(StoreWindows.SeriesWindow);
        public bool OpenStory => _navigationService.DialogActive(StoreWindows.StoryWindow);

        public string DebugState => _configuration.GetConnectionString("ComicsStore");

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
    }
}
