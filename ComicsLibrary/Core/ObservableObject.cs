using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComicsLibrary.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        private bool _isDirty;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        protected void Set<T>(ref T input, T value, [CallerMemberName] string info = null)
        {
            input = value;
            RaisePropertyChanged(info);
        }

        public virtual bool IsDirty { get => _isDirty; set { Set(ref _isDirty, value); RaisePropertyChanged("IsClean"); } }
        public bool IsClean { get => !IsDirty; }
    }
}
