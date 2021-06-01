using ComicsStore.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class StoryArtist : CrossTable
    {
        public StoryArtist() : base()
        {
        }

        public int StoryId { get; set; }
        public int ArtistId { get; set; }
        [EnumDataType(typeof(ArtistType), ErrorMessage = "Artist type value doesn't exist within enum")]
        public ArtistType ArtistType { get; set; }

        public Artist Artist { get; set; }
        public Story Story { get; set; }
    }
}