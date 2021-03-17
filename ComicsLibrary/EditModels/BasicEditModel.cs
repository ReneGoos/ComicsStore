using AutoMapper;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComicsLibrary.EditModels
{
    public abstract class BasicEditModel : INotifyPropertyChanged
    {
        public BasicEditModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public DateTime CreationDate { get; protected set; }
        public DateTime DateUpdate { get; protected set; }
    }
}
