using System.Linq;

public class FriendlyCharactersFinder : MultipleCharactersFinder
{
    private void Start()
    {
        FindFriendlyCharacters();
    }

    private void FindFriendlyCharacters()
    {
        Characters?.ForEach((tag) => tag.Health.Dead -= FindFriendlyCharacters);
        Characters = Character.Active.Where((tag) => tag.CompareTag(gameObject.tag));
        Characters?.ForEach((tag) => tag.Health.Dead += FindFriendlyCharacters);
    }
}
