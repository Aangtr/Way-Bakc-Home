using System.Collections;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public int damagePerTick = 1;
    public float damageInterval = 1f;

    private PlayerHealth playerHealth;
    private Coroutine damageCoroutine;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            if (damageCoroutine == null)
                damageCoroutine = StartCoroutine(WaterDamageLoop());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator WaterDamageLoop()
    {
        while (true)
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(damagePerTick);

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
