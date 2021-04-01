using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class PublisherEditModel : TableEditModel
    {
        private ICollection<BookPublisherEditModel> _bookPublishers;

        public ICollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set { Set(ref _bookPublishers, value); } }
    }
}
