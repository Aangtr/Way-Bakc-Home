using UnityEngine;

public class EnemyMeelee : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1f;

    public int damage = 10;
    public float attackCooldown = 1f;

    private Transform player;
    private Rigidbody2D rb;
    private float nextAttackTime;
    private bool facingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= chaseRange && dist > attackRange)
        {
            ChasePlayer();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if (dist <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void ChasePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(dir.x * moveSpeed, rb.linearVelocity.y);

        if (dir.x > 0 && !facingRight)
            Flip();
        else if (dir.x < 0 && facingRight)
            Flip();
    }

    void Attack()
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.TakeDamage(damage);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
