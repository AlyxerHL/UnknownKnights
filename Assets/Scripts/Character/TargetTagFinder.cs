using UnityEngine;

public class TargetTagFinder : MonoBehaviour
{
    public TargetTag TargetTag { get; private set; }

    private void Start()
    {
        TargetTag = TargetTag.ActiveTargetTags.MinBy(
            (e) => (transform.position - e.transform.position).sqrMagnitude
        );
    }
}
