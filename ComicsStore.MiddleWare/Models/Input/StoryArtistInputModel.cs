using ComicsStore.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryArtistInputModel : BasicInputModel
    {
        [Required(ErrorMessage = "Artist Name is required"),MaxLength(255)]
        public string ArtistName { get; set; }
        public ArtistType ArtistType { get; set; }
    }
}
