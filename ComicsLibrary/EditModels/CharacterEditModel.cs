using ComicsLibrary.Core;
using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class CharacterEditModel : TableEditModel
    {
        private ObservableChangedCollection<StoryCharacterEditModel> _storyCharacters;

        public CharacterEditModel() : base()
        {
            StoryCharacter = new ObservableChangedCollection<StoryCharacterEditModel>();
        }

        public ObservableChangedCollection<StoryCharacterEditModel> StoryCharacter { get => _storyCharacters; set => Set(ref _storyCharacters, value); }

        public void AddStoryCharacter(int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryCharacter.Any(s => s.StoryId == storyId.Value))
                {
                    StoryCharacter.Add(new StoryCharacterEditModel
                    {
                        CharacterId = Id,
                        StoryId = storyId
                    });
                }
            }
        }

        public List<StoryCharacterEditModel> GetStoryCharacters()
        {
            return new List<StoryCharacterEditModel>(StoryCharacter.ToList().ConvertAll(s => new StoryCharacterEditModel
            {
                CharacterId = s.CharacterId,
                StoryId = s.StoryId
            }));
        }
    }
}
