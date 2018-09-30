using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Artist
    {
        public Artist()
        {
            StoryArtist = new HashSet<StoryArtist>();

            CreationDate = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Artist Name is required"),MaxLength(255)]
        public string ArtistName { get; set; }
        public string Remark { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateUpdate { get; set; }

        public ICollection<StoryArtist> StoryArtist { get; set; }
    }
}
