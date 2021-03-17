using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ComicsLibrary.EditModels
{
    public class PublisherEditModel : TableEditModel
    {
        private ICollection<BookPublisherEditModel> _bookPublishers;

        public ICollection<BookPublisherEditModel> BookPublishers
        {
            get => _bookPublishers;
            set
            {
                _bookPublishers = value;
                RaisePropertyChanged();
            }
        }
    }
}
