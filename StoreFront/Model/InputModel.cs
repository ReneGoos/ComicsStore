using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Model
{
    public abstract class InputModel : INotifyPropertyChanged
    {
        private string name;
        private string remark;

        public InputModel()
        {
            IsDirty = false;
        }

        public bool IsDirty { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Remark
        {
            get => remark;
            set
            {
                remark = value;
                OnPropertyChanged();
            }
        }
    }
}
