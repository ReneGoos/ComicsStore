using ComicsStore.Data.Model.Interfaces;
using System.Collections.Generic;

namespace ComicsStore.Data.Model
{
    public class Artist : MainTable, IStoryArtist
    {
        public Artist()
            : base()
        {
            StoryArtist = new HashSet<StoryArtist>();
        }

        public ICollection<StoryArtist> StoryArtist { get; set; }
    }
}
