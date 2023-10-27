public class NearestTargetTagFinder : TargetTagFinder
{
    private void Start()
    {
        FindTargetTag();
    }

    private void FindTargetTag()
    {
        TargetTag.Health.OnDeath -= FindTargetTag;
        TargetTag = TargetTag.ActiveTargetTags.MinBy(
            (e) => (transform.position - e.transform.position).sqrMagnitude
        );
        TargetTag.Health.OnDeath += FindTargetTag;
    }
}
