using ComicsLibrary.Core;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class CharacterEditModel : CharacterOnlyEditModel
    {
        private ObservableChangedCollection<CharacterStoryEditModel> _storyCharacters;

        public CharacterEditModel() : base()
        {
            StoryCharacter = new ObservableChangedCollection<CharacterStoryEditModel>();
        }

        public ObservableChangedCollection<CharacterStoryEditModel> StoryCharacter { get => _storyCharacters; set => Set(ref _storyCharacters, value); }

        public void HandleStory(int? oldStoryId, StoryOnlyEditModel story)
        {
            StoryCharacter.HandleItem(Id, oldStoryId, story);
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
