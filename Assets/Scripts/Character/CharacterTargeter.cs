using UnityEngine;

public class CharacterTargeter : MonoBehaviour
{
    public TargetableCharacter Target { get; private set; }

    public void FindTarget()
    {
        Target = TargetableCharacter.ActiveEntities.MinBy(
            (e) => (transform.position - e.transform.position).sqrMagnitude
        );
    }
}
