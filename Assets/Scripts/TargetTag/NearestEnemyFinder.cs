using System.Linq;

public class NearestEnemyFinder : TargetTagFinder
{
    private void Start()
    {
        FindTargetTag();
    }

    private void FindTargetTag()
    {
        if (TargetTag != null)
        {
            TargetTag.Health.OnDeath -= FindTargetTag;
        }

        TargetTag = TargetTag.ActiveTargetTags
            .Where((tag) => !tag.IsFriendly)
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (TargetTag != null)
        {
            TargetTag.Health.OnDeath += FindTargetTag;
        }
    }
}
