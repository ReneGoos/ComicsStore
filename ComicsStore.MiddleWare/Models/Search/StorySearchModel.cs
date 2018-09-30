using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Models.Search
{
    public class StorySearchModel : BasicSearchModel
    {
        public StoryType StoryType { get; set; }
        public string ExtraInfo { get; set; }
    }
}
