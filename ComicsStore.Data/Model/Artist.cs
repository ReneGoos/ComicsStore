using System.Collections.Generic;

namespace ComicsStore.Data.Model
{
    public class Artist : MainTable
    {
        public Artist()
            : base()
        {
            StoryArtist = new HashSet<StoryArtist>();
        }
        
        public ICollection<StoryArtist> StoryArtist { get; set; }
    }
}
