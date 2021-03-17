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
    public class SeriesEditModel : TableEditModel
    {
        private ICollection<BookSeriesEditModel> _bookSeries;
        private int? _seriesNumber;
        private string _seriesLanguage;

        public int? SeriesNumber { get => _seriesNumber; set { _seriesNumber = value; RaisePropertyChanged(); } }
        public string SeriesLanguage { get => _seriesLanguage; set { _seriesLanguage = value; RaisePropertyChanged(); } }
        public ICollection<BookSeriesEditModel> BookSeries
        {
            get => _bookSeries;
            set
            {
                _bookSeries = value;
                RaisePropertyChanged();
            }
        }
    }
}
