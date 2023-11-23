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

    public static readonly Vector2 TopPosition = new(-6f, 0.5f);
    public static readonly Vector2 BottomPosition = new(-6f, -1.5f);
    public static readonly Vector2 BackPosition = new(-8f, -0.5f);
}
