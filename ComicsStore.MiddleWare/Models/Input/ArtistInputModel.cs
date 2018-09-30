using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class ArtistInputModel : BasicInputModel
    {
        [Required(ErrorMessage = "Artist Name is required"),MaxLength(255)]
        public string ArtistName { get; set; }
    }
}
