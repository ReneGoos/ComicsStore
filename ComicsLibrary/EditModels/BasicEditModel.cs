using ComicsLibrary.Core;
using System;

namespace ComicsLibrary.EditModels
{
    public abstract class BasicEditModel : ObservableObject
    {
        public BasicEditModel()
        {
        }

        public DateTime CreationDate { get; protected set; }
        public DateTime DateUpdate { get; protected set; }
    }
}
