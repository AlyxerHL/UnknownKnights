using System.Linq;

public class FriendlyCharactersFinder : MultipleCharactersFinder
{
    private void Start()
    {
        FindNearestEnemy();
    }

    private void FindNearestEnemy()
    {
        Tags = CharacterTag.ActiveTargetTags.Where((tag) => tag.CompareTag(gameObject.tag));
    }
}
