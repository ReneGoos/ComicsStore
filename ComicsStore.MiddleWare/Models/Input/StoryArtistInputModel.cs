using ComicsStore.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class StoryArtistInputModel : BasicInputModel
    {
        public ArtistType ArtistType { get; set; }
    }
}
