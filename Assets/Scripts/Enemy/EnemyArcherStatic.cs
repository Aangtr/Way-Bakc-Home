using UnityEngine;

public class EnemyArcherStatic : MonoBehaviour
{
    public float shootCooldown = 2f;
    public float shootRange = 6f;

    public GameObject arrowPrefab;
    public Transform firePoint;

    private float timer;
    private Transform player;
    private bool facingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > shootRange) return;

        FacePlayer();

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Shoot();
            timer = shootCooldown;
        }
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = (player.position - firePoint.position).normalized;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir * 8f;
    }
}
