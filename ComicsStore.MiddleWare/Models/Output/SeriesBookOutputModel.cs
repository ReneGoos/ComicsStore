using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class SeriesBookOutputModel : BasicOutputModel
    {
        public string SeriesNr { get; set; }
        public int? SeriesOrder { get; set; }
        public string BookName { get; set; }
        public BookType BookType { get; set; }
        public Active Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public int? FirstPrint { get; set; }
    }
}
