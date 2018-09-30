using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class CharacterInputModel : BasicInputModel
    {
        [Required(ErrorMessage = "Character Name is required"), MaxLength(255)]
        public string CharacterName { get; set; }
    }
}
