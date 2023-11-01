using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using System;
using ComicsStore.Data.Common;

namespace ComicsLibrary.ViewModels
{
    public class SeriesViewModel : BasicTableViewModel<ISeriesService, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearch, SeriesEditModel>
    {
        private readonly IBooksService _booksService;
        private readonly ICodesService _codesService;

        public ICommand DeleteBookFromListCommand { get; protected set; }

        public SeriesViewModel(ISeriesService seriesService,
            IBooksService booksService,
            ICodesService codesService,
            INavigationService navigationService,
            IMapper mapper) : base(seriesService, navigationService, mapper)
        {
            DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
            _booksService = booksService;
            _codesService = codesService;
        }

        public async void HandleBook(int? bookId, int? oldBookId)
        {
            var book = bookId.HasValue ? Mapper.Map<BookOnlyEditModel>(await _booksService.GetAsync(bookId.Value)) : null;
            IsDirty = IsDirty || Item.HandleBook(oldBookId, book);
        }

        public void DeleteBookFromList(int? bookId)
        {
            IsDirty = IsDirty || Item.HandleBook(bookId, null);
        }

        public async void HandleCode(int? codeId, int? oldCodeId)
        {
            var code = codeId.HasValue ? Mapper.Map<CodeOnlyEditModel>(await _codesService.GetAsync(codeId.Value)) : null;
            IsDirty = IsDirty || Item.HandleCode(oldCodeId, code);
        }

        public void DeleteCode(int? codeId)
        {
            IsDirty = IsDirty || Item.HandleCode(codeId, null);
        }

        public override void ItemChange(TableType table, int? id, ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.deleteItem:
                    switch (table)
                    {
                        case TableType.book:
                            DeleteBookFromList(id);
                            break;

                        case TableType.code:
                            DeleteCode(id);
                            break;
                    }
                    break;

                case ActionType.updateItem:
                    switch (table)
                    {
                        case TableType.book:
                            HandleBook(id, id);
                            break;

                        case TableType.code:
                            HandleCode(id, id);
                            break;
                    }
                    break;
            }
        }
    }
}