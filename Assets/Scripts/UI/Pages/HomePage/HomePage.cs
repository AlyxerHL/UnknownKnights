using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{
    [SerializeField]
    private SlotAvatar[] slotAvatarPrefabs;

    [SerializeField]
    private RectTransform slotAvatarContainer;

    private void Awake()
    {
        foreach (var prefab in slotAvatarPrefabs)
        {
            var slotAvatar = Instantiate(prefab, slotAvatarContainer);
            slotAvatar.Selected += (character) => Debug.Log(character.name);
        }
    }

    public void StartBattle()
    {
        SceneManager.LoadScene(1);
    }
}
