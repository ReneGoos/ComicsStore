using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class BasicOutputModel
    {
        public int Id { get; set; }
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
