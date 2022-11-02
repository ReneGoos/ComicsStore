using System;

namespace ComicsLibrary.EditModels
{
    public interface IBasicEditModel
    {
        string this[string columnName] { get; }

        DateTime CreationDate { get; }
        DateTime DateUpdate { get; }
        string Error { get; set; }

        bool Validate();
    }
}