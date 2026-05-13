using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroKnight : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check (Raycast)")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int attackDamage = 15;
    public float attackCooldown = 0.3f;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    private bool isGrounded;
    private float nextAttackTime;

    PlayerCombat combat;


    private void Start()
    {
        combat = GetComponent<PlayerCombat>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ===== MOVE INPUT =====
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // ===== GROUND CHECK =====
        isGrounded = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        // ===== JUMP =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // ===== ATTACK =====
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }

        // ===== ANIMATION =====
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            animator.SetBool("Grounded", isGrounded);
            animator.SetFloat("VelocityY", rb.linearVelocity.y);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Attack()
    {
        Debug.Log("ATTACK INPUT OK");

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        Debug.Log("HIT COUNT: " + hits.Length);

        foreach (Collider2D hit in hits)
        {
            Debug.Log("HIT: " + hit.name);

            EnemySimple enemy = hit.GetComponentInParent<EnemySimple>();
            Debug.Log("EnemySimple found: " + (enemy != null));
            EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }
    public void DoAttack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        Debug.Log("HIT COUNT: " + hits.Length);

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                groundCheck.position,
                groundCheck.position + Vector3.down * groundCheckDistance
            );
        }

        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
