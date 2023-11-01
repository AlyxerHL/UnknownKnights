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
            .Where((character) => !character.CompareTag(gameObject.tag))
            .MinBy((character) => (transform.position - character.transform.position).sqrMagnitude);

        if (Character != null)
        {
            Character.Health.Dead += FindNearestEnemyCharacter;
        }
    }
}
