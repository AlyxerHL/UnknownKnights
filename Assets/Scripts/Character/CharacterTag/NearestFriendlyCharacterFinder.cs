using System.Linq;

public class NearestFriendlyCharacterFinder : SingleCharacterFinder
{
    private void Start()
    {
        FindNearestFriendlyCharacter();
    }

    private void FindNearestFriendlyCharacter()
    {
        if (Tag != null)
        {
            Tag.Health.Dead -= FindNearestFriendlyCharacter;
        }

        Tag = CharacterTag.ActiveTags
            .Where((tag) => tag.gameObject != gameObject)
            .Where((tag) => tag.CompareTag(gameObject.tag))
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (Tag != null)
        {
            Tag.Health.Dead += FindNearestFriendlyCharacter;
        }
    }
}
