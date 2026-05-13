using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public float deathJumpForce = 6f;
    public float deathSpinForce = 200f;
    public float destroyDelay = 2f;

    Rigidbody2D rb;
    Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void Die()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * deathJumpForce, ForceMode2D.Impulse);
            rb.AddTorque(deathSpinForce);
        }

        if (col != null)
            col.enabled = false;

        // Tắt AI / script khác
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
        {
            if (s != this)
                s.enabled = false;
        }

        Destroy(gameObject, destroyDelay);
    }
}
