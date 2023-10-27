using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private TargetTagFinder targetTagFinder;

    private bool IsWithinTargetDistance =>
        (targetTagFinder.TargetTag.transform.position - transform.position).sqrMagnitude
        < targetDistance;

    private void Update()
    {
        if (targetTagFinder.TargetTag == null || IsWithinTargetDistance)
        {
            return;
        }

        var direction = (
            targetTagFinder.TargetTag.transform.position - transform.position
        ).normalized;
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
