namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookOnlyOutputModel : BasicOutputModel
    {
        public string BookType { get; set; }
        public string Active { get; set; }
        public int FirstYear { get; set; }
        public int? ThisYear { get; set; }
        public string FirstPrint { get; set; }
        public string Signed { get; set; }
        public string Checked { get; set; }
        public string CoverType { get; set; }
    }
}
