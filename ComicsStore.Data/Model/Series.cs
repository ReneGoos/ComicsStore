using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Series
    {
        public Series()
        {
            BookSeries = new HashSet<BookSeries>();

            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Series Name is required"), MaxLength(255)]
        public string SeriesName { get; set; }
        public string Remark { get; set; }
        public int? SeriesNumber { get; set; }
        [Required(ErrorMessage = "Two character language id is required"), MaxLength(2)]
        [RegularExpression("^[a-z]{2}$", ErrorMessage = "Language must be two lowercase characters")]
        public string SeriesLanguage { get; set; }
        public int CodeId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Code Code { get; set; }
        public ICollection<BookSeries> BookSeries { get; set; }
    }
}