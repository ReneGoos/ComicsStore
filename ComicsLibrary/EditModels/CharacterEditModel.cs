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

        public void AddStoryCharacter(ObservableChangedCollection<StoryCharacterEditModel> storyCharacters, int? storyId)
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

        public ObservableChangedCollection<StoryCharacterEditModel> GetStoryCharacters()
        {
            return new ObservableChangedCollection<StoryCharacterEditModel>(StoryCharacter.ToList().ConvertAll(s => new StoryCharacterEditModel
            {
                CharacterId = s.CharacterId,
                StoryId = s.StoryId
            }));
        }

        public override void ResetId()
        {
            Id = null;

            foreach (var story in StoryCharacter)
            {
                story.CharacterId = null;
            }
        }
    }
}
