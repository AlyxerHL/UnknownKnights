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
            .Where((tag) => tag.gameObject != gameObject)
            .Where((tag) => tag.CompareTag(gameObject.tag))
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (Character != null)
        {
            Character.Health.Dead += FindNearestFriendlyCharacter;
        }
    }
}
