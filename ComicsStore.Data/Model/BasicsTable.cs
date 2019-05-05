using System;

namespace ComicsStore.Data.Model
{
    public abstract class BasicsTable
    {
        public BasicsTable()
        {
            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
