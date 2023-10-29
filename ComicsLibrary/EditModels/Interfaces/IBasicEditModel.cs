using System;
using System.Collections.Generic;

namespace ComicsLibrary.EditModels.Interfaces
{
    public interface IBasicEditModel
    {
        //string this[string columnName] { get; }

        DateTime CreationDate { get; }
        DateTime DateUpdate { get; }

        bool Validate(Dictionary<string, List<string>> errors);
    }
}