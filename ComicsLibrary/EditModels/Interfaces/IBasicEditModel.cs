using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComicsLibrary.EditModels.Interfaces
{
    public interface IBasicEditModel : INotifyPropertyChanged
    {
        //string this[string columnName] { get; }

        DateTime CreationDate { get; }
        DateTime DateUpdate { get; }

        bool Validate(Dictionary<string, List<string>> errors);
    }
}