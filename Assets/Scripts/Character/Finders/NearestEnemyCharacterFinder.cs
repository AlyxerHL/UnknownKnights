using System.Linq;

public class NearestEnemyCharacterFinder : SingleCharacterFinder
{
    private void Start()
    {
        FindNearestEnemyCharacter();
    }

    private void FindNearestEnemyCharacter()
    {
        if (Character != null)
        {
            Character.Health.Dead -= FindNearestEnemyCharacter;
        }

        Character = Character.Active
            .Where((tag) => !tag.CompareTag(gameObject.tag))
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (Character != null)
        {
            Character.Health.Dead += FindNearestEnemyCharacter;
        }
    }
}
