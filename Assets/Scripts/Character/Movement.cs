using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private TargetTag targetTag;

    private void Start()
    {
        targetTag = TargetTag.ActiveTargetTags.MinBy(
            (e) => (transform.position - e.transform.position).sqrMagnitude
        );
    }

    private void Update()
    {
        // 대충 타겟이 있으면 타겟 방향으로 이동하는 코드
    }
}
