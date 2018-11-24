using System;

namespace ComicsStore.MiddleWare.Models.Search
{
    public class BasicSearchModel
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
