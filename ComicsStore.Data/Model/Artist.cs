using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.Data.Model
{
    public class Artist : MainTable, IStoryArtist, IMainArtist, IPseudonymArtist
    {
        public Artist()
            : base()
        {
            StoryArtist = new HashSet<StoryArtist>();
            PseudonymArtist = new HashSet<Pseudonym>();
            MainArtist = new HashSet<Pseudonym>();
        }

        public string FullName { get; private set; }
        public string FirstName { get; set; }
        [EnumDataType(typeof(YesNoInd), ErrorMessage = "Value doesn't exist within enum")]
        public YesNoInd Pseudonym { get; set; }
        public string RealLastName { get; set; }
        public string RealFirstName { get; set; }

        public ICollection<StoryArtist> StoryArtist { get; set; }
        public ICollection<Pseudonym> PseudonymArtist { get; set; }
        public ICollection<Pseudonym> MainArtist { get; set; }
    }
}
