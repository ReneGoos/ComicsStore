using System;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class BasicOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
