using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 3f;
    public float wallCheckDistance = 0.6f;
    public LayerMask groundLayer;

    [Header("Attack")]
    public float attackCooldown = 1.5f;
    public GameObject arrowPrefab;
    public Transform firePoint;

    [Header("Ledge Check")]
    public float ledgeCheckX = 0.5f;
    public float ledgeCheckY = 1f;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2(moveSpeed * (facingRight ? 1 : -1), rb.linearVelocity.y);

        Vector2 dir = facingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wall = Physics2D.Raycast(transform.position, dir, wallCheckDistance, groundLayer);

        Vector2 ledgeStart = (Vector2)transform.position + new Vector2(ledgeCheckX * (facingRight ? 1 : -1), 0);
        RaycastHit2D ledge = Physics2D.Raycast(ledgeStart, Vector2.down, ledgeCheckY, groundLayer);

        if (wall.collider != null || ledge.collider == null)
            Flip();
    }

    public void Shoot()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        if (!facingRight)
            arrow.transform.Rotate(0, 180, 0);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
