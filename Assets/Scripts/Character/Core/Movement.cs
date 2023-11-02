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
        (tagFinder.Character.transform.position - transform.position).sqrMagnitude < targetDistance;

    private void Update()
    {
        if (tagFinder.Character == null || BattleTime.IsPaused)
        {
            return;
        }

        if (!IsWithinTargetDistance)
        {
            var direction = (
                tagFinder.Character.transform.position - transform.position
            ).normalized;
            transform.Translate(speed * Time.deltaTime * direction);
        }

        spriteRenderer.flipX = tagFinder.Character.transform.position.x < transform.position.x;
    }
}
