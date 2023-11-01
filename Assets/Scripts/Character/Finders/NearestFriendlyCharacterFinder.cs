using System.Linq;

public class NearestFriendlyCharacterFinder : SingleCharacterFinder
{
    private void Start()
    {
        FindNearestFriendlyCharacter();
    }

    private void FindNearestFriendlyCharacter()
    {
        if (Character != null)
        {
            Character.Health.Dead -= FindNearestFriendlyCharacter;
        }

        Character = Character.Active
            .Where((character) => character.gameObject != gameObject)
            .Where((character) => character.CompareTag(gameObject.tag))
            .MinBy((character) => (transform.position - character.transform.position).sqrMagnitude);

        if (Character != null)
        {
            Character.Health.Dead += FindNearestFriendlyCharacter;
        }
    }
}
