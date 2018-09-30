using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class CharacterOutputModel : BasicOutputModel
    {
        public string CharacterName { get; set; }

        public ICollection<StoryOutputModel> StoryCharacter { get; set; }
    }
}
