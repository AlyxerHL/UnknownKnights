using System.Linq;

public class NearestEnemyCharacterFinder : SingleCharacterFinder
{
    private void Start()
    {
        FindNearestEnemyCharacter();
    }

    private void FindNearestEnemyCharacter()
    {
        if (Tag != null)
        {
            Tag.Health.OnDeath -= FindNearestEnemyCharacter;
        }

        Tag = CharacterTag.ActiveTags
            .Where((tag) => !tag.CompareTag(gameObject.tag))
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (Tag != null)
        {
            Tag.Health.OnDeath += FindNearestEnemyCharacter;
        }
    }
}
