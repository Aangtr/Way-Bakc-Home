using UnityEngine;

public class GoalTrigger2D : MonoBehaviour
{
    [Header("Requirement")]
    [SerializeField] private int requiredKeys = 4;  // set ngoài Inspector
    [SerializeField] private bool oneShot = true;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered && oneShot) return;
        if (!other.CompareTag("Player")) return;

        // lấy inventory trên Player
        PlayerInventory inv = other.GetComponent<PlayerInventory>();
        if (inv == null)
        {
            Debug.LogWarning("[GoalTrigger2D] PlayerInventory not found on Player!");
            return;
        }

        if (inv.keyCount >= requiredKeys)
        {
            triggered = true;

            // chặn spam trigger
            var col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            VictoryManager.Instance?.ShowVictory();
        }
        else
        {
            // thiếu key -> thông báo (tuỳ chọn)
            VictoryManager.Instance?.ShowNeedKeys();
            Debug.Log($"Chưa đủ key! ({inv.keyCount}/{requiredKeys})");
        }
    }
}
