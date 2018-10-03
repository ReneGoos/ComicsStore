using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookSeriesInputModel : BasicInputModel
    {
        public string SeriesNr { get; set; }
        public int? SeriesOrder { get; set; }
        public int? SeriesNumber { get; set; }
        [Required(ErrorMessage = "Two character language id is required"), MaxLength(2)]
        [RegularExpression("^[a-z]{2}$", ErrorMessage = "Language must be two lowercase characters")]
        public string SeriesLanguage { get; set; }

        public CodeInputModel Code { get; set; }
    }
}