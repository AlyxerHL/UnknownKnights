using System.Collections.Generic;
using UnityEngine;

public abstract class MultipleCharactersFinder : MonoBehaviour
{
    public IEnumerable<Character> Characters { get; protected set; }
}
