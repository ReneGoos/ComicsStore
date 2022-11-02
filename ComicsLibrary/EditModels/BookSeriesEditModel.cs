using ComicsStore.Data.Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class BookSeriesEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _bookId;
        private int? _seriesId;
        private string _issue;
        private decimal? _seriesOrder;

        public int? BookId { get => _bookId; set => SetIfValue(ref _bookId, value); }
        public int? SeriesId { get => _seriesId; set => SetIfValue(ref _seriesId, value); }
        [Required]
        public string Issue
        {
            get => _issue;
            set
            {
                Set(ref _issue, value);
                if (_seriesOrder == null)
                {
                    SeriesOrder = decimal.Parse(new String(_issue.Where(c => (Char.IsDigit(c) || c.Equals('.'))).ToArray()));
                }
            }
        }

        [Required]
        public decimal? SeriesOrder { get => _seriesOrder; set => Set(ref _seriesOrder, value); }
        public SeriesOnlyEditModel Series { get; set; }

        public int? MainId { get => BookId; set => BookId = value; }
        public int? LinkedId { get => SeriesId; set => SeriesId = value; }
        public TableEditModel ChildItem { get => Series; set => Series = value as SeriesOnlyEditModel; }
    }
}