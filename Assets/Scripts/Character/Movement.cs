using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private TargetTagFinder tagFinder;

    private bool IsWithinTargetDistance =>
        (tagFinder.TargetTag.transform.position - transform.position).sqrMagnitude < targetDistance;

    private void Update()
    {
        if (tagFinder.TargetTag == null || IsWithinTargetDistance)
        {
            return;
        }

        var direction = (tagFinder.TargetTag.transform.position - transform.position).normalized;
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
