using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 10;
    public float lifeTime = 3f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Xoay mũi tên theo hướng bay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponentInParent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
