using System.Collections.Generic;
using UnityEngine;

public abstract class MultipleCharactersFinder : MonoBehaviour
{
    public IEnumerable<CharacterTag> Tags { get; protected set; }
}
