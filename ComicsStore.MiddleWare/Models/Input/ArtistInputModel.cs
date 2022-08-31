using ComicsStore.Data.Common;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class ArtistInputModel : BasicInputModel, IArtistInputModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Pseudonym { get; set; }
        public string RealLastName { get; set; }
        public string RealFirstName { get; set; }

        public ICollection<StoryArtistInputModel> StoryArtist { get; set; }
        public ICollection<PseudonymInputModel> MainArtist { get; set; }
        public ICollection<PseudonymInputModel> PseudonymArtist { get; set; }
    }
}
