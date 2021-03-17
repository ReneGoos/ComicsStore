using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ComicsLibrary.EditModels
{
    public class BookEditModel : TableEditModel
    {
        private ICollection<BookPublisherEditModel> _bookPublishers;
        private ICollection<BookSeriesEditModel> _bookSeries;
        private ICollection<StoryBookEditModel> _storyBooks;
        private string _bookType;
        private string _active;
        private int _firstYear;
        private int? _thisYear;
        private string _firstPrint;

        public string BookType { get => _bookType; set { _bookType = value; RaisePropertyChanged(); } }
        public string Active { get => _active; set { _active = value; RaisePropertyChanged(); } }
        public int FirstYear { get => _firstYear; set { _firstYear = value; RaisePropertyChanged(); } }
        public int? ThisYear { get => _thisYear; set { _thisYear = value; RaisePropertyChanged(); } }
        public string FirstPrint { get => _firstPrint; set { _firstPrint = value; RaisePropertyChanged(); } }

        public ICollection<BookPublisherEditModel> BookPublishers
        {
            get => _bookPublishers;
            set
            {
                _bookPublishers = value;
                RaisePropertyChanged();
            }
        }

        public ICollection<BookSeriesEditModel> BookSeries
        {
            get => _bookSeries;
            set
            {
                _bookSeries = value;
                RaisePropertyChanged();
            }
        }

        public ICollection<StoryBookEditModel> StoryBooks
        {
            get => _storyBooks;
            set
            {
                _storyBooks = value;
                RaisePropertyChanged();
            }
        }
    }
}
