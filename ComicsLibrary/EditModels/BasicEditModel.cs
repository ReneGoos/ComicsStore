using ComicsLibrary.Core;
using System;

namespace ComicsLibrary.EditModels
{
    public abstract class BasicEditModel : ObservableObject
    {
        private DateTime _creationDate;
        private DateTime _dateUpdate;

        public BasicEditModel()
        {
        }

        public DateTime CreationDate { get => _creationDate; protected set { Set(ref _creationDate, value); } }
        public DateTime DateUpdate { get => _dateUpdate; protected set { Set(ref _dateUpdate, value); } }
    }
}
