using UnityEngine;

public class EnemyArcherDeath : MonoBehaviour
{
    public float deathJumpForce = 6f;
    public float deathSpinForce = 200f;
    public float destroyDelay = 2f;

    private Rigidbody2D rb;
    private Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void DieMarioStyle()
    {
        // Tắt collider & AI
        if (col != null) col.enabled = false;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
        {
            if (s != this) s.enabled = false;
        }

        // Reset velocity
        rb.linearVelocity = Vector2.zero;

        // BẬT LÊN
        rb.AddForce(Vector2.up * deathJumpForce, ForceMode2D.Impulse);

        // XOAY
        rb.AddTorque(deathSpinForce);

        Destroy(gameObject, destroyDelay);
    }
}
