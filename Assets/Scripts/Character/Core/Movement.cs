using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private TargetFinder tagFinder;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private bool IsWithinTargetDistance =>
        (tagFinder.TargetTag.transform.position - transform.position).sqrMagnitude < targetDistance;

    private void Update()
    {
        if (tagFinder.TargetTag == null)
        {
            return;
        }

        if (!IsWithinTargetDistance)
        {
            var direction = (
                tagFinder.TargetTag.transform.position - transform.position
            ).normalized;
            transform.Translate(speed * Time.deltaTime * direction);
        }

        spriteRenderer.flipX = tagFinder.TargetTag.transform.position.x < transform.position.x;
    }
}
