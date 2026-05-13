using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int keyCount;

    public void AddKey(int amount = 1)
    {
        keyCount += amount;
        Debug.Log("Nhặt key, tổng: " + keyCount);

        // sau này gọi UI update ở đây
    }
}
