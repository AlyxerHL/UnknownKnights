using System.Linq;

public class FriendlyCharactersFinder : MultipleCharactersFinder
{
    private void Start()
    {
        FindFriendlyCharacters();
    }

    private void FindFriendlyCharacters()
    {
        Characters?.ForEach((character) => character.Health.Dead -= FindFriendlyCharacters);
        Characters = Character.Active.Where((character) => character.CompareTag(gameObject.tag));
        Characters?.ForEach((character) => character.Health.Dead += FindFriendlyCharacters);
    }
}
