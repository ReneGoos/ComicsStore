using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using System.ComponentModel;

namespace ComicsLibrary.EditModels
{
    public class CharacterEditModel : CharacterOnlyEditModel
    {
        private ObservableChangedCollection<CharacterStoryEditModel> _storyCharacters;

        public CharacterEditModel() : base()
        {
            StoryCharacter = [];
        }

        public ObservableChangedCollection<CharacterStoryEditModel> StoryCharacter { get => _storyCharacters; set => Set(ref _storyCharacters, value); }

        public bool HandleStory(int? oldStoryId, StoryOnlyEditModel story, PropertyChangedEventHandler propertyChanged = null)
        {
            return StoryCharacter.HandleItem(Id, oldStoryId, story, propertyChanged);
        }

        public void ResetId()
        {
            Id = null;

            foreach (var story in StoryCharacter)
            {
                story.CharacterId = null;
            }
        }
    }
}
