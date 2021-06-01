using ComicsLibrary.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace ComicsLibrary.Helpers
{
    public class PagingCollectionView<T> : CollectionView
        where T : ICollectionItem
    {
        private readonly IList<T> _innerList;
        private readonly int _itemsPerPage;

        private int _currentPage = 1;

        public ICommand OnNextClicked { get; protected set; }
        public ICommand OnPreviousClicked { get; protected set; }
        public ICommand OnFirstClicked { get; protected set; }
        public ICommand OnLastClicked { get; protected set; }

        public PagingCollectionView(IList<T> innerList, int itemsPerPage)
            : base(innerList)
        {
            _innerList = innerList;
            _itemsPerPage = itemsPerPage;
            OnNextClicked = new RelayCommand(new Action(MoveToNextPage));
            OnPreviousClicked = new RelayCommand(new Action(MoveToPreviousPage));
            OnFirstClicked = new RelayCommand(new Action(MoveToFirstPage));
            OnLastClicked = new RelayCommand(new Action(MoveToLastPage));
        }

        public override int Count
        {
            get
            {
                if (_innerList.Count == 0) return 0;
                if (_currentPage < PageCount) // page 1..n-1
                {
                    return _itemsPerPage;
                }
                else // page n
                {
                    var itemsLeft = _innerList.Count % _itemsPerPage;
                    if (0 == itemsLeft)
                    {
                        return _itemsPerPage; // exactly itemsPerPage left
                    }
                    else
                    {
                        // return the remaining items
                        return itemsLeft;
                    }
                }
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentPage)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FirstPage)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(LastPage)));
            }
        }

        public int ItemsPerPage { get { return _itemsPerPage; } }

        public int PageCount
        {
            get
            {
                return (_innerList.Count + _itemsPerPage - 1)
                    / _itemsPerPage;
            }
        }

        public int EndIndex
        {
            get
            {
                var end = _currentPage * _itemsPerPage - 1;
                return (end > _innerList.Count) ? _innerList.Count : end;
            }
        }

        public int StartIndex
        {
            get
            {
                return (_currentPage - 1) * _itemsPerPage;
            }
        }

        public override object GetItemAt(int index)
        {
            var offset = index % (_itemsPerPage);
            return _innerList[StartIndex + offset];
        }

        public void MoveToFirstPage()
        {
            CurrentPage = 1;
            Refresh();
        }

        public void MoveToLastPage()
        {
            CurrentPage = PageCount;
            Refresh();
        }

        public void MoveToNextPage()
        {
            if (_currentPage < PageCount)
            {
                CurrentPage += 1;
            }
            Refresh();
        }

        public void MoveToPreviousPage()
        {
            if (_currentPage > 1)
            {
                CurrentPage -= 1;
            }
            Refresh();
        }

        public bool LastPage { get => _currentPage == PageCount; }
        public bool FirstPage { get => _currentPage == 1; }
    }
}
