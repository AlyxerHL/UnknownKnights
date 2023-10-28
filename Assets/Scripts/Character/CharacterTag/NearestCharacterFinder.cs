using System.Linq;
using UnityEngine;

public class NearestCharacterFinder : CharacterFinder
{
    [SerializeField]
    private bool findFriendly;

    private void Start()
    {
        FindNearestEnemy();
    }

    private void FindNearestEnemy()
    {
        Tag = CharacterTag.ActiveTargetTags
            .Where((tag) => tag.IsFriendly == findFriendly)
            .MinBy((tag) => (transform.position - tag.transform.position).sqrMagnitude);

        if (Tag != null)
        {
            Tag.Health.OnDeath += FindNearestEnemy;
        }
    }
}
