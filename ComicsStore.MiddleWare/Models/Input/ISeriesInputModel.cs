using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public interface ISeriesInputModel : IBasicInputModel
    {
        ICollection<BookSeriesInputModel> BookSeries { get; set; }
        CodeInputModel Code { get; set; }
        int CodeId { get; set; }
        string SeriesLanguage { get; set; }
        int? SeriesNumber { get; set; }
    }
}