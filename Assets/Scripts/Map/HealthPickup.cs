using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20;
    public AudioClip pickupSound;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(healAmount);
            }

            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            collected = true;
            Destroy(gameObject);
        }
    }
}
