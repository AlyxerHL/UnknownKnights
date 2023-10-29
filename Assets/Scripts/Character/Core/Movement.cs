using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private SingleCharacterFinder tagFinder;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private bool IsWithinTargetDistance =>
        (tagFinder.Tag.transform.position - transform.position).sqrMagnitude < targetDistance;

    private void Update()
    {
        if (tagFinder.Tag == null)
        {
            return;
        }

        if (!IsWithinTargetDistance)
        {
            var direction = (
                tagFinder.Tag.transform.position - transform.position
            ).normalized;
            transform.Translate(speed * Time.deltaTime * direction);
        }

        spriteRenderer.flipX = tagFinder.Tag.transform.position.x < transform.position.x;
    }
}
