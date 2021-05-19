using ComicsStore.Data.Model.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Series : MainTable, IBookSeries
    {
        public Series()
            : base()
        {
            BookSeries = new HashSet<BookSeries>();
        }

        public int? SeriesNumber { get; set; }
        [Required(ErrorMessage = "Two character language id is required"), MaxLength(2)]
        [RegularExpression("^[a-z]{2}$", ErrorMessage = "Language must be two lowercase characters")]
        public string SeriesLanguage { get; set; }
        public int CodeId { get; set; }

        public Code Code { get; set; }
        public ICollection<BookSeries> BookSeries { get; set; }
    }
}