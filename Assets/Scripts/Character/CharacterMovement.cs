using UnityEngine;

[RequireComponent(typeof(CharacterTargeter))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private CharacterTargeter targeter;

    private void Awake()
    {
        targeter = GetComponent<CharacterTargeter>();
    }

    private void Update()
    {
        // 대충 타겟이 있으면 타겟 방향으로 이동하는 코드
    }
}
