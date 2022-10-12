using ComicsLibrary.Core;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class CharacterEditModel : TableEditModel
    {
        private ObservableChangedCollection<CharacterStoryEditModel> _storyCharacters;

        public CharacterEditModel() : base()
        {
            StoryCharacter = new ObservableChangedCollection<CharacterStoryEditModel>();
        }

        public ObservableChangedCollection<CharacterStoryEditModel> StoryCharacter { get => _storyCharacters; set => Set(ref _storyCharacters, value); }

        public void AddStoryCharacter(int? storyId, int? oldStoryId)
        {
            StoryCharacter.HandleItem(Id, storyId, oldStoryId);
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
