using System;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class StoryArtist
    {
        public StoryArtist()
        {
            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int StoryId { get; set; }
        public int ArtistId { get; set; }
        [EnumDataType(typeof(ArtistType), ErrorMessage = "Artist type value doesn't exist within enum")]
        public ArtistType ArtistType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public Artist Artist { get; set; }
        public Story Story { get; set; }
    }
}