using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.ViewModels
{
    public class ComicsViewModel : BasicViewModel
    {
        private readonly IArtistsService _artistsService;
        private readonly IBooksService _booksService;
        private readonly ICharactersService _charactersService;
        private readonly ICodesService _codesService;
        private readonly IPublishersService _publishersService;
        private readonly ISeriesService _seriesService;
        private readonly IStoriesService _storiesService;
        private readonly IMapper _mapper;

        private List<string> FillStoryTypes()
        {
            var storyTypes = new List<string>();

            foreach (var value in Enum.GetValues(typeof(StoryType))
                                    .Cast<StoryType>()
                                    .Select(v => v.ToString()))
            {   storyTypes.Add(value);
            }

            return storyTypes;
        }

        public ComicsViewModel(IArtistsService artistsService,
            IBooksService booksService,
            ICharactersService charactersService,
            ICodesService codesService,
            IPublishersService publishersService,
            ISeriesService seriesService,
            IStoriesService storiesService,
            IMapper mapper) : base(mapper)
        {
            _artistsService = artistsService;
            _booksService = booksService;
            _charactersService = charactersService;
            _codesService = codesService;
            _publishersService = publishersService;
            _seriesService = seriesService;
            _storiesService = storiesService;
            _mapper = mapper;

            ArtistView = new ArtistViewModel(artistsService, mapper);
            BookView = new BookViewModel(booksService, mapper);
            CharacterView = new CharacterViewModel(charactersService, mapper);
            CodeView = new CodeViewModel(codesService, mapper);
            PublisherView = new PublisherViewModel(publishersService, mapper);
            SeriesView = new SeriesViewModel(seriesService, mapper);
            StoryView = new StoryViewModel(storiesService, mapper);

            Languages = LanguageType.FillLanguages();
            StoryTypes = FillStoryTypes();
        }

        public ArtistViewModel ArtistView { get; private set; }
        public BookViewModel BookView { get; private set; }
        public CharacterViewModel CharacterView { get; private set; }
        public CodeViewModel CodeView { get; private set; }
        public PublisherViewModel PublisherView { get; private set; }
        public SeriesViewModel SeriesView { get; private set; }
        public StoryViewModel StoryView { get; private set; }

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
