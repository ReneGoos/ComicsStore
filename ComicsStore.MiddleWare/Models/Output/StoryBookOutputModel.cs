using ComicsStore.Data.Model;
using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class StoryBookOutputModel : BasicCrossOutputModel
    {
        public int BookId { get; set; }

        public BookOnlyOutputModel Book { get; set; }
    }
}
