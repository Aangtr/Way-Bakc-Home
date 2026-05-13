using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    [Header("Invincibility")]
    public float invincibleTime = 1f;
    private bool isInvincible;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.Init(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    // 👉 Dùng khi LOAD GAME hoặc RESET
    public void RefreshUI()
    {
        if (healthBar != null)
        {
            healthBar.Init(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isInvincible) return;

        currentHealth -= dmg;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        Debug.Log("Player mất máu: " + dmg);

        StartCoroutine(InvincibleCoroutine());

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("PLAYER DIE");

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.ShowGameOver();

        gameObject.SetActive(false); // disable player
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        Debug.Log("Player hồi máu: +" + amount);
    }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
}
