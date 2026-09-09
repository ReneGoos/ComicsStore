using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsLibrary.Navigation;
using ComicsLibrary.ViewModels.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using SoftGoosR.Common.Core;
using SoftGoosR.Windows.Core;
using System.Globalization;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels;

public class ComicsViewModel : ObservableObject, IActivable
{
    private readonly ComicsStoreDbContext _comicsStoreDbContext;
    private readonly INavigationService _navigationService;
    private readonly IConfiguration _configuration;

    public ArtistViewModel ArtistView { get; private set; }
    public ArtistViewModel PseudonymArtistView { get; private set; }
    public BookViewModel BookView { get; private set; }
    public CharacterViewModel CharacterView { get; private set; }
    public CodeViewModel CodeView { get; private set; }
    public PublisherViewModel PublisherView { get; private set; }
    public SeriesViewModel SeriesView { get; private set; }
    public StoryViewModel StoryView { get; private set; }
    public StoryViewModel OriginStoryView { get; private set; }
    public ReportViewModel ReportView { get; private set; }
    public InformationViewModel InformationView { get; private set; }

    public List<string> Actives { get; set; }
    public List<string> BookTypes { get; set; }
    public List<string> CoverTypes { get; set; }
    public List<string> StoryTypes { get; set; }
    public List<string> YesNoInds { get; set; }
    public List<LanguageType> Languages { get; set; }

    public bool LastArtist => _navigationService.LastPageActive(StoreWindows.Artist);
    public bool LastBook => _navigationService.LastPageActive(StoreWindows.Book);
    public bool LastCharacter => _navigationService.LastPageActive(StoreWindows.Character);
    public bool LastCode => _navigationService.LastPageActive(StoreWindows.Code);
    public bool LastOriginStory => _navigationService.LastPageActive(StoreWindows.OriginStory);
    public bool LastPseudonymArtist => _navigationService.LastPageActive(StoreWindows.PseudonymArtist);
    public bool LastPublisher => _navigationService.LastPageActive(StoreWindows.Publisher);
    public bool LastSeries => _navigationService.LastPageActive(StoreWindows.Series);
    public bool LastStory => _navigationService.LastPageActive(StoreWindows.Story);

    public bool OpenArtist => _navigationService.PageActive(StoreWindows.Artist);
    public bool OpenBook => _navigationService.PageActive(StoreWindows.Book);
    public bool OpenCharacter => _navigationService.PageActive(StoreWindows.Character);
    public bool OpenCode => _navigationService.PageActive(StoreWindows.Code);
    public bool OpenOriginStory => _navigationService.PageActive(StoreWindows.OriginStory);
    public bool OpenPseudonymArtist => _navigationService.PageActive(StoreWindows.PseudonymArtist);
    public bool OpenPublisher => _navigationService.PageActive(StoreWindows.Publisher);
    public bool OpenSeries => _navigationService.PageActive(StoreWindows.Series);
    public bool OpenStory => _navigationService.PageActive(StoreWindows.Story);
    public string PageChain => _navigationService.PageChain;

    public string DebugState
    {
        get
        {
            var connectionString = _configuration.GetConnectionString("ComicsStore");
            return connectionString[connectionString.LastIndexOf('\\')..];
        }
    }

    public ICommand ShowArtistFromStoryWindowCommand { get; protected set; }
    public ICommand ShowArtistFromOriginStoryWindowCommand { get; protected set; }
    public ICommand ShowMainArtistFromArtistWindowCommand { get; protected set; }
    public ICommand ShowPseudonymArtistFromArtistWindowCommand { get; protected set; }
    public ICommand ShowBookFromPublisherWindowCommand { get; protected set; }
    public ICommand ShowBookFromSeriesWindowCommand { get; protected set; }
    public ICommand ShowBookFromOriginStoryWindowCommand { get; protected set; }
    public ICommand ShowBookFromStoryWindowCommand { get; protected set; }
    public ICommand ShowCharacterFromOriginStoryWindowCommand { get; protected set; }
    public ICommand ShowCharacterFromStoryWindowCommand { get; protected set; }
    public ICommand ShowCodeFromSeriesWindowCommand { get; protected set; }
    public ICommand ShowCodeFromOriginStoryWindowCommand { get; protected set; }
    public ICommand ShowCodeFromStoryWindowCommand { get; protected set; }
    public ICommand ShowPublisherFromBookWindowCommand { get; protected set; }
    public ICommand ShowSeriesFromBookWindowCommand { get; protected set; }
    public ICommand ShowSeriesFromCodeWindowCommand { get; protected set; }
    public ICommand ShowOriginStoryFromStoryWindowCommand { get; protected set; }
    public ICommand ShowStoryOriginFromStoryWindowCommand { get; protected set; }
    public ICommand ShowStoryFromArtistWindowCommand { get; protected set; }
    public ICommand ShowStoryFromPseudonymArtistWindowCommand { get; protected set; }
    public ICommand ShowStoryFromBookWindowCommand { get; protected set; }
    public ICommand ShowStoryFromCharacterWindowCommand { get; protected set; }
    public ICommand ShowStoryFromCodeWindowCommand { get; protected set; }

    public ICommand ShowArtistWindowCommand { get; protected set; }
    public ICommand ShowBookWindowCommand { get; protected set; }
    public ICommand ShowCharacterWindowCommand { get; protected set; }
    public ICommand ShowCodeWindowCommand { get; protected set; }
    public ICommand ShowOriginStoryWindowCommand { get; protected set; }
    public ICommand ShowPseudonymArtistWindowCommand { get; protected set; }
    public ICommand ShowPublisherWindowCommand { get; protected set; }
    public ICommand ShowSeriesWindowCommand { get; protected set; }
    public ICommand ShowStoryWindowCommand { get; protected set; }
    public ICommand ShowReportWindowCommand { get; protected set; }

    public ICommand ShowBookInformationWindowCommand { get; protected set; }
    public ICommand ShowArtistInformationWindowCommand { get; protected set; }
    public ICommand ShowCharacterInformationWindowCommand { get; protected set; }
    public ICommand ShowCodeInformationWindowCommand { get; protected set; }
    public ICommand ShowPublisherInformationWindowCommand { get; protected set; }
    public ICommand ShowSeriesInformationWindowCommand { get; protected set; }
    public ICommand ShowStoryInformationWindowCommand { get; protected set; }
    public ICommand CheckArtistDuplicatesCommand { get; protected set; }

    public ComicsViewModel(ComicsStoreDbContext comicsStoreDbContext,
        IArtistsService artistsService,
        IBooksService booksService,
        ICharactersService charactersService,
        ICodesService codesService,
        IPublishersService publishersService,
        ISeriesService seriesService,
        IStoriesService storiesService,
        IViewService exportBooksService,
        IInformationService informationViewService,
        IMapper mapper,
        INavigationService navigationService,
        IConfiguration configuration) : base()
    {
        _comicsStoreDbContext = comicsStoreDbContext;

        _comicsStoreDbContext.Database.EnsureCreated();

        _navigationService = navigationService;
        _navigationService.PropertyChanged += NavigationService_PropertyChanged;

        _configuration = configuration;
        ArtistView = new ArtistViewModel(artistsService, storiesService, navigationService, mapper);
        ArtistView.ItemChanged += ArtistView_ItemChanged;
        BookView = new BookViewModel(booksService, publishersService, seriesService, storiesService, navigationService, mapper);
        BookView.ItemChanged += BookView_ItemChanged;
        CharacterView = new CharacterViewModel(charactersService, storiesService, navigationService, mapper);
        CharacterView.ItemChanged += CharacterView_ItemChanged;
        CodeView = new CodeViewModel(codesService, seriesService, storiesService, navigationService, mapper);
        CodeView.ItemChanged += CodeView_ItemChanged;
        PublisherView = new PublisherViewModel(publishersService, booksService, navigationService, mapper);
        PublisherView.ItemChanged += PublisherView_ItemChanged;
        SeriesView = new SeriesViewModel(seriesService, booksService, codesService, navigationService, mapper);
        SeriesView.ItemChanged += SeriesView_ItemChanged;
        StoryView = new StoryViewModel(storiesService, artistsService, booksService, charactersService, codesService, navigationService, mapper);
        StoryView.ItemChanged += StoryView_ItemChanged;

        PseudonymArtistView = new ArtistViewModel(artistsService, storiesService, navigationService, mapper);
        PseudonymArtistView.ItemChanged += PseudonymArtistView_ItemChanged;
        OriginStoryView = new StoryViewModel(storiesService, artistsService, booksService, charactersService, codesService, navigationService, mapper);
        OriginStoryView.ItemChanged += OriginStoryView_ItemChanged;

        InformationView = new InformationViewModel(informationViewService, mapper);
        ReportView = new ReportViewModel(exportBooksService, mapper);

        Languages = LanguageType.FillLanguages();
        Actives = FillEnum<Active>();
        BookTypes = FillEnum<BookType>();
        CoverTypes = FillEnum<CoverType>();
        StoryTypes = FillEnum<StoryType>();
        YesNoInds = FillEnum<YesNoInd>();

        ShowArtistFromOriginStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowArtistFromOriginStoryWindow));
        ShowArtistFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowArtistFromStoryWindow));                         
        ShowMainArtistFromArtistWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowMainArtistFromArtistWindow));
        ShowPseudonymArtistFromArtistWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowPseudonymArtistFromArtistWindow));
        ShowBookFromPublisherWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowBookFromPublisherWindow));
        ShowBookFromSeriesWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowBookFromSeriesWindow));
        ShowBookFromOriginStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowBookFromOriginStoryWindow));
        ShowBookFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowBookFromStoryWindow));
        ShowCharacterFromOriginStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCharacterFromOriginStoryWindow));
        ShowCharacterFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCharacterFromStoryWindow));
        ShowCodeFromSeriesWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCodeFromSeriesWindow));
        ShowCodeFromOriginStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCodeFromOriginStoryWindow));
        ShowCodeFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowCodeFromStoryWindow));
        ShowPublisherFromBookWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowPublisherFromBookWindow));
        ShowSeriesFromBookWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowSeriesFromBookWindow));
        ShowSeriesFromCodeWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowSeriesFromCodeWindow));
        ShowOriginStoryFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowOriginStoryFromStoryWindow));
        ShowStoryOriginFromStoryWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryOriginFromStoryWindow));
        ShowStoryFromArtistWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromArtistWindow));
        ShowStoryFromPseudonymArtistWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromPseudonymArtistWindow));
        ShowStoryFromBookWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromBookWindow));
        ShowStoryFromCharacterWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromCharacterWindow));
        ShowStoryFromCodeWindowCommand = new RelayCommand<int?>(new Action<int?>(ShowStoryFromCodeWindow));

        ShowArtistWindowCommand = new RelayCommand(new Action(ShowArtistWindow));
        ShowBookWindowCommand = new RelayCommand(new Action(ShowBookWindow));
        ShowCharacterWindowCommand = new RelayCommand(new Action(ShowCharacterWindow));
        ShowCodeWindowCommand = new RelayCommand(new Action(ShowCodeWindow));
        ShowOriginStoryWindowCommand = new RelayCommand(new Action(ShowOriginStoryWindow));
        ShowPseudonymArtistWindowCommand = new RelayCommand(new Action(ShowPseudonymArtistWindow));
        ShowPublisherWindowCommand = new RelayCommand(new Action(ShowPublisherWindow));
        ShowSeriesWindowCommand = new RelayCommand(new Action(ShowSeriesWindow));
        ShowStoryWindowCommand = new RelayCommand(new Action(ShowStoriesWindow));
        ShowReportWindowCommand = new RelayCommand(new Action(ShowReportWindow));

        ShowArtistInformationWindowCommand = new RelayCommand<int>(new Action<int>(ShowArtistInformationWindow));
        ShowBookInformationWindowCommand = new RelayCommand<int>(new Action<int>(ShowBookInformationWindow));
        ShowCharacterInformationWindowCommand = new RelayCommand<int>(new Action<int>(ShowCharacterInformationWindow));
        ShowCodeInformationWindowCommand = new RelayCommand<int>(new Action<int>(ShowCodeInformationWindow));
        ShowPublisherInformationWindowCommand = new RelayCommand<int>(new Action<int>(ShowPublisherInformationWindow));
        ShowSeriesInformationWindowCommand = new RelayCommand<int>(new Action<int>(ShowSeriesInformationWindow));
        ShowStoryInformationWindowCommand = new RelayCommand<int>(new Action<int>(ShowStoryInformationWindow));

        CheckArtistDuplicatesCommand = new RelayCommand(new Action(CheckArtistDuplicates));
    }

    private void ArtistView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        PseudonymArtistView.ItemChange(TableType.artist, e.Id, e.ActionType);
        StoryView.ItemChange(TableType.artist, e.Id, e.ActionType);
        OriginStoryView.ItemChange(TableType.artist, e.Id, e.ActionType);
    }

    private void PseudonymArtistView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        ArtistView.ItemChange(TableType.artist, e.Id, e.ActionType);
        StoryView.ItemChange(TableType.artist, e.Id, e.ActionType);
        OriginStoryView.ItemChange(TableType.artist, e.Id, e.ActionType);
    }

    private void BookView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        PublisherView.ItemChange(TableType.book, e.Id, e.ActionType);
        SeriesView.ItemChange(TableType.book, e.Id, e.ActionType);
        StoryView.ItemChange(TableType.book, e.Id, e.ActionType);
        OriginStoryView.ItemChange(TableType.book, e.Id, e.ActionType);
    }

    private void CharacterView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        StoryView.ItemChange(TableType.character, e.Id, e.ActionType);
        OriginStoryView.ItemChange(TableType.character, e.Id, e.ActionType);
    }

    private void CodeView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        SeriesView.ItemChange(TableType.code, e.Id, e.ActionType);
        StoryView.ItemChange(TableType.code, e.Id, e.ActionType);
        OriginStoryView.ItemChange(TableType.code, e.Id, e.ActionType);
    }

    private void PublisherView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        BookView.ItemChange(TableType.publisher, e.Id, e.ActionType);
    }

    private void SeriesView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        BookView.ItemChange(TableType.series, e.Id, e.ActionType);
        CodeView.ItemChange(TableType.series, e.Id, e.ActionType);
    }

    private void StoryView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        ArtistView.ItemChange(TableType.story, e.Id, e.ActionType);
        PseudonymArtistView.ItemChange(TableType.story, e.Id, e.ActionType);
        BookView.ItemChange(TableType.story, e.Id, e.ActionType);
        CharacterView.ItemChange(TableType.story, e.Id, e.ActionType);
        CodeView.ItemChange(TableType.story, e.Id, e.ActionType);
        OriginStoryView.ItemChange(TableType.story, e.Id, e.ActionType);
    }

    private void OriginStoryView_ItemChanged(object sender, ItemChangedEventArgs e)
    {
        ArtistView.ItemChange(TableType.story, e.Id, e.ActionType);
        PseudonymArtistView.ItemChange(TableType.story, e.Id, e.ActionType);
        BookView.ItemChange(TableType.story, e.Id, e.ActionType);
        CharacterView.ItemChange(TableType.story, e.Id, e.ActionType);
        CodeView.ItemChange(TableType.story, e.Id, e.ActionType);
        StoryView.ItemChange(TableType.story, e.Id, e.ActionType);
    }

    private void NavigationService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("ActivePages"))
        {
            RaisePropertyChanged("OpenArtist");
            RaisePropertyChanged("OpenBook");
            RaisePropertyChanged("OpenCharacter");
            RaisePropertyChanged("OpenCode");
            RaisePropertyChanged("OpenOriginStory");
            RaisePropertyChanged("OpenPseudonymArtist");
            RaisePropertyChanged("OpenPublisher");
            RaisePropertyChanged("OpenSeries");
            RaisePropertyChanged("OpenStory");
            RaisePropertyChanged("LastArtist");
            RaisePropertyChanged("LastBook");
            RaisePropertyChanged("LastCharacter");
            RaisePropertyChanged("LastCode");
            RaisePropertyChanged("LastOriginStory");
            RaisePropertyChanged("LastPseudonymArtist");
            RaisePropertyChanged("LastPublisher");
            RaisePropertyChanged("LastSeries");
            RaisePropertyChanged("LastStory");
            RaisePropertyChanged("PageChain");
        }
    }

    public override bool IsDirty
    {
        get
        {
            return ArtistView.IsDirty ||
                BookView.IsDirty ||
                CharacterView.IsDirty ||
                CodeView.IsDirty ||
                PublisherView.IsDirty ||
                SeriesView.IsDirty ||
                StoryView.IsDirty;
        }
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

    private static List<string> FillEnum<T>() where T : Enum
    {
        return [.. EnumHelper<T>.GetNames()];
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

    private static void GetList<TEdit, TOut>(IBasicTableViewModel<TEdit, TOut> itemView, int? itemId)
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

    private async void ShowArtistWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.Artist, null, null);
    }

    private async void ShowBookWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.Book, null, null);
    }

    private async void ShowCharacterWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.Character, null, null);
    }

    private async void ShowCodeWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.Code, null, null);
    }

    private async void ShowOriginStoryWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.OriginStory, null, null);
    }

    private async void ShowPseudonymArtistWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.PseudonymArtist, null, null);
    }

    private async void ShowPublisherWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.Publisher, null, null);
    }

    private async void ShowSeriesWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.Series, null, null);
    }

    private async void ShowStoriesWindow()
    {
        _ = await _navigationService.ShowPageAsync(StoreWindows.Story, null, null);
    }

    private async void ShowArtistFromOriginStoryWindow(int? itemId)
    {
        GetItem(ArtistView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Artist, itemId, OriginStoryView.HandleArtist);
    }

    private async void ShowArtistFromStoryWindow(int? itemId)
    {
        GetItem(ArtistView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Artist, itemId, StoryView.HandleArtist);
    }

    private async void ShowPseudonymArtistFromArtistWindow(int? itemId)
    {
        GetItem(PseudonymArtistView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.PseudonymArtist, itemId, ArtistView.HandlePseudonymArtist);
    }

    private async void ShowMainArtistFromArtistWindow(int? itemId)
    {
        GetItem(PseudonymArtistView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.PseudonymArtist, itemId, ArtistView.HandleMainArtist);
    }
    private void CheckArtistDuplicates()
    {
        var artists = ArtistView.Items;
        var artistsEquals = new Dictionary<int, int>();

        foreach (var artist in artists)
        {
            foreach (var artistCompare in artists)
            {
                if (artistCompare.Id > artist.Id && string.Compare(artistCompare.Name, artist.Name, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
                {
                    artistsEquals.Add(artistCompare.Id, artist.Id);
                }
            }
        }

        var count = artistsEquals.Count;
        //return artistsEquals.Any();
    }

    private async void ShowBookFromPublisherWindow(int? itemId)
    {
        GetItem(BookView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Book, itemId, PublisherView.HandleBook);
    }

    private async void ShowBookFromSeriesWindow(int? itemId)
    {
        GetItem(BookView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Book, itemId, SeriesView.HandleBook);
    }

    private async void ShowBookFromOriginStoryWindow(int? itemId)
    {
        GetItem(BookView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Book, itemId, OriginStoryView.HandleBook);
    }

    private async void ShowBookFromStoryWindow(int? itemId)
    {
        GetItem(BookView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Book, itemId, StoryView.HandleBook);
    }

    private async void ShowCharacterFromOriginStoryWindow(int? itemId)
    {
        GetItem(CharacterView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Character, itemId, OriginStoryView.HandleCharacter);
    }

    private async void ShowCharacterFromStoryWindow(int? itemId)
    {
        GetItem(CharacterView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Character, itemId, StoryView.HandleCharacter);
    }

    private async void ShowCodeFromSeriesWindow(int? itemId)
    {
        GetItem(CodeView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Code, itemId, SeriesView.HandleCode);
    }

    private async void ShowCodeFromOriginStoryWindow(int? itemId)
    {
        GetItem(CodeView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Code, itemId, OriginStoryView.HandleCode);
    }

    private async void ShowCodeFromStoryWindow(int? itemId)
    {
        GetItem(CodeView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Code, itemId, StoryView.HandleCode);
    }

    private async void ShowPublisherFromBookWindow(int? itemId)
    {
        GetItem(PublisherView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Publisher, itemId, BookView.HandlePublisher);
    }

    private async void ShowSeriesFromBookWindow(int? itemId)
    {
        GetItem(SeriesView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Series, itemId, BookView.HandleSeries);
    }
    private async void ShowSeriesFromCodeWindow(int? itemId)
    {
        GetItem(SeriesView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Series, itemId, CodeView.HandleSeries);
    }

    private async void ShowOriginStoryFromStoryWindow(int? itemId)
    {
        GetItem(OriginStoryView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.OriginStory, itemId, StoryView.HandleOriginStory);
    }

    private async void ShowStoryOriginFromStoryWindow(int? itemId)
    {
        GetItem(OriginStoryView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.OriginStory, itemId, StoryView.HandleStoryOrigin);
    }

    private async void ShowStoryFromArtistWindow(int? itemId)
    {
        GetItem(StoryView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Story, itemId, ArtistView.HandleStory); 
    }

    private async void ShowStoryFromPseudonymArtistWindow(int? itemId)
    {
        GetItem(StoryView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Story, itemId, PseudonymArtistView.HandleStory);
    }

    private async void ShowStoryFromBookWindow(int? itemId)
    {
        GetItem(StoryView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Story, itemId, BookView.HandleStory);
    }

    private async void ShowStoryFromCharacterWindow(int? itemId)
    {
        GetItem(StoryView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Story, itemId, CharacterView.HandleStory);
    }

    private async void ShowStoryFromCodeWindow(int? itemId)
    {
        GetItem(StoryView, itemId);

        _ = await _navigationService.ShowPageAsync(StoreWindows.Story, itemId, CodeView.HandleStory);
    }

    private async void ShowReportWindow()
    {
        await _navigationService.ShowWindowAsync(StoreWindows.Report);
    }

    private async Task ShowInformationWindow(IdSearch search)
    {
        search.Active = null;
        search.Filter = null;

        InformationView.Search = search;

        await _navigationService.ShowWindowAsync(StoreWindows.Informational);
//        await _navigationService.ShowWindowAsync(StoreWindows.Information);
    }

    private async void ShowArtistInformationWindow(int itemId)
    {
        await ShowInformationWindow(new IdSearch { ArtistId = itemId });
    }

    private async void ShowBookInformationWindow(int itemId)
    {
        await ShowInformationWindow(new IdSearch { BookId = itemId });
    }

    private async void ShowCharacterInformationWindow(int itemId)
    {
        await ShowInformationWindow(new IdSearch { CharacterId = itemId });
    }

    private async void ShowCodeInformationWindow(int itemId)
    {
        await ShowInformationWindow(new IdSearch { CodeId = itemId });
    }

    private async void ShowPublisherInformationWindow(int itemId)
    {
        await ShowInformationWindow(new IdSearch { PublisherId = itemId });
    }

    private async void ShowSeriesInformationWindow(int itemId)
    {
        await ShowInformationWindow(new IdSearch { SeriesId = itemId });
    }

    private async void ShowStoryInformationWindow(int itemId)
    {
        await ShowInformationWindow(new IdSearch { StoryId = itemId });
    }

    public Task ActivateAsync(object parameter)
    {
        return Task.CompletedTask;
    }
}
