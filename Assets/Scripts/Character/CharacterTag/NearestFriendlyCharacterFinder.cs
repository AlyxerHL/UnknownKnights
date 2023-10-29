using System.Linq;

public class NearestFriendlyCharacterFinder : SingleCharacterFinder
{
    private void Start()
    {
        FindNearestEnemy();
    }

    private void FindNearestEnemy()
    {
        Tag = CharacterTag.ActiveTargetTags
            .Where((tag) => tag.CompareTag(gameObject.tag))
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (Tag != null)
        {
            Tag.Health.OnDeath += FindNearestEnemy;
        }
    }
}
