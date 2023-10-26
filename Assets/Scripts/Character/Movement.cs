using UnityEngine;

[RequireComponent(typeof(TargetTagFinder))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private TargetTagFinder targeter;

    private void Awake()
    {
        targeter = GetComponent<TargetTagFinder>();
    }

    private void Update()
    {
        // 대충 타겟이 있으면 타겟 방향으로 이동하는 코드
    }
}
