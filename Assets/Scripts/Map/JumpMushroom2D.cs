using UnityEngine;

public class JumpMushroom2D : MonoBehaviour
{
    public float bounceForce = 12f; // Lực bật lên

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Reset lực rơi trước khi bật
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

                // Bật lên
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
