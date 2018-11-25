using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class SeriesInputModel : BasicInputModel
    {
        public int? SeriesNumber { get; set; }
        [Required(ErrorMessage = "Two character language id is required"), MaxLength(2)]
        [RegularExpression("^[a-z]{2}$", ErrorMessage = "Language must be two lowercase characters")]
        public string SeriesLanguage { get; set; }

        public int CodeId { get; set; }
        public CodeInputModel Code { get; set; }
    }
}