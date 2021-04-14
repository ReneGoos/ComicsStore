using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class CharacterEditModel : TableEditModel
    {
        private ICollection<StoryCharacterEditModel> _storyCharacters;

        public CharacterEditModel() : base()
        {
            StoryCharacter = new List<StoryCharacterEditModel>();
        }

        public ICollection<StoryCharacterEditModel> StoryCharacter { get => _storyCharacters; set { Set(ref _storyCharacters, value); } }
    }
}
