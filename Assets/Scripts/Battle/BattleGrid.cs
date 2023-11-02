using UnityEngine;

[CreateAssetMenu(fileName = "BattleGrid", menuName = "BattleGrid")]
public class BattleGrid : ScriptableObject
{
    [field: SerializeField]
    public Character Top { get; set; }

    [field: SerializeField]
    public Character Bottom { get; set; }

    [field: SerializeField]
    public Character Back { get; set; }
}
