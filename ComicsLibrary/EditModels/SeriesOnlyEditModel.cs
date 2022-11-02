using ComicsStore.MiddleWare.Models.Output;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.EditModels
{
    public class SeriesOnlyEditModel : TableEditModel
    {
        private int? _seriesNumber;
        private string _seriesLanguage;
        private int _codeId;

        [Required]
        public int? SeriesNumber { get => _seriesNumber; set => Set(ref _seriesNumber, value); }
        [Required]
        public string SeriesLanguage { get => _seriesLanguage; set => Set(ref _seriesLanguage, value); }
        [Required]
        public int CodeId { get => _codeId; set => Set(ref _codeId, value); }
    }
}