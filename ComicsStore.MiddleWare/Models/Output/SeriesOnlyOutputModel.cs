using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesOnlyOutputModel : BasicOutputModel
    {
        public int? SeriesNumber { get; set; }
        public string SeriesLanguage { get; set; }

        public CodeOnlyOutputModel SeriesCode { get; set; }
    }
}