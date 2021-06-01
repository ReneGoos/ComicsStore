using System;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IBasicOutputModel
    {
        DateTime CreationDate { get; set; }
        DateTime DateUpdate { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string Remark { get; set; }
    }
}