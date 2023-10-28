using System.Linq;

public class NearestEnemyFinder : TargetFinder
{
    private void Start()
    {
        FindNearestEnemy();
    }

    private void FindNearestEnemy()
    {
        TargetTag = TargetTag.ActiveTargetTags
            .Where((tag) => !tag.IsFriendly)
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (TargetTag != null)
        {
            TargetTag.Health.OnDeath += FindNearestEnemy;
        }
    }
}
