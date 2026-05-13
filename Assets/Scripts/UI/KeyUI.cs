using TMPro;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    public PlayerInventory player;
    public TextMeshProUGUI keyText;

    void Update()
    {
        keyText.text = "Key: " + player.keyCount;
    }
}
