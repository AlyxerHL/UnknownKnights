using UnityEngine;

public class CharacterTargeter : MonoBehaviour
{
    public TargetableCharacter Target { get; private set; }

    private void Start()
    {
        Target = TargetableCharacter.ActiveEntities.MinBy(
            (e) => (transform.position - e.transform.position).sqrMagnitude
        );
    }
}
