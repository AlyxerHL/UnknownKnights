using System.Linq;

public class FriendlyCharactersFinder : MultipleCharactersFinder
{
    private void Start()
    {
        FindFriendlyCharacters();
    }

    private void FindFriendlyCharacters()
    {
        Tags.ForEach((tag) => tag.Health.OnDeath -= FindFriendlyCharacters);
        Tags = CharacterTag.ActiveTags.Where((tag) => tag.CompareTag(gameObject.tag));
        Tags.ForEach((tag) => tag.Health.OnDeath += FindFriendlyCharacters);
    }
}
