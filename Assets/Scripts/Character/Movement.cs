using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float distanceToTarget;

    [SerializeField]
    private TargetTagFinder targetTagFinder;

    private void Update()
    {
        // 대충 타겟이 있으면 타겟 방향으로 이동하는 코드
    }
}
