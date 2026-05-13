using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack")]
    public Collider2D attackHitbox;     // collider gắn ở AttackPoint
    public int attackDamage = 15;
    public LayerMask enemyLayer;
    public float attackCooldown = 0.35f;

    private Animator animator;
    private float nextAttackTime;
    private bool isAttacking;

    void Awake()
    {
        animator = GetComponent<Animator>();

        // đảm bảo hitbox tắt lúc đầu
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    void Update()
    {
        HandleAttackInput();
    }

    void HandleAttackInput()
    {
        if (Time.time < nextAttackTime) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldown;

        animator.SetTrigger("Attack1"); // giữ đúng tên Animator của bạn
    }

    // ===== ANIMATION EVENTS =====

    // gọi ở frame chém TRÚNG
    public void AE_EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = true;
    }

    // gọi ngay sau frame chém
    public void AE_DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    // gọi ở cuối animation Attack
    public void AE_EndAttack()
    {
        isAttacking = false;
    }

    // ===== HIT ENEMY =====

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!attackHitbox.enabled) return;
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }

    // để HeroKnight hỏi trạng thái
    public bool IsAttacking()
    {
        return isAttacking;
    }
}
