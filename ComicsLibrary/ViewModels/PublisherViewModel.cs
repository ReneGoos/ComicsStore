using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsLibrary.ListViewModels;
using ComicsLibrary.Navigation;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using SoftGoosR.Windows.Core;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels;

public class PublisherViewModel : BasicTableViewModel<IPublishersService, PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearch, PublisherEditModel>
{
    private readonly IBooksService _booksService;

    public ICommand DeleteBookFromListCommand { get; protected set; }

    public PublisherViewModel(IPublishersService publishersService,
        IBooksService booksService,
        INavigationService navigationService,
        IMapper mapper,
        IBasicListViewModel<PublisherOutputModel> publishers) : base(publishersService, navigationService, mapper, publishers)
    {
        DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
        _booksService = booksService;
    }

    public async void HandleBook(int? bookId, int? oldBookId)
    {
        var book = bookId.HasValue ? Mapper.Map<BookOnlyEditModel>(await _booksService.GetAsync(bookId.Value)) : null;
        IsDirty |= Item.HandleBook(oldBookId, book, ItemPropertyChanged);
    }

    public void DeleteBookFromList(int? bookId)
    {
        IsDirty |= Item.HandleBook(bookId, null);
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
                }
                break;

            case ActionType.updateItem:
                switch (table)
                {
                    case TableType.book:
                        HandleBook(id, id);
                        break;
                }
                break;
        }
    }
}