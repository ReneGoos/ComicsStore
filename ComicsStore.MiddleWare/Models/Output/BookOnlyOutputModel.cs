using ComicsStore.Data.Model;
using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookOnlyOutputModel : BasicOutputModel
    {
        public string BookType { get; set; }
        public string Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public int? FirstPrint { get; set; }
    }
}
