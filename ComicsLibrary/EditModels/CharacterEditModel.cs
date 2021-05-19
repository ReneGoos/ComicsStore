using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddStoryCharacter(ICollection<StoryCharacterEditModel> storyCharacters, int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryCharacter.Any(s => s.StoryId == storyId.Value))
                {
                    if (!storyCharacters.Any(s => s.StoryId == storyId.Value))
                    {
                        storyCharacters.Add(new StoryCharacterEditModel
                        {
                            CharacterId = Id,
                            StoryId = storyId
                        });
                    }

                    StoryCharacter = storyCharacters;
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
