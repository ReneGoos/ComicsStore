using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class StoryArtistEditModel : CrossEditModel
    {
        private int _artistId;
        private int _storyId;
        private ICollection<string> _artistType;

        public int ArtistId { get => _artistId; set { _artistId = value; RaisePropertyChanged(); }}
        public int StoryId { get => _storyId; set  { _storyId = value; RaisePropertyChanged(); }}
        public ICollection<string> ArtistType { get => _artistType; set  { _artistType = value; RaisePropertyChanged(); }}
    }
}