using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private float shrinkDuration = 0.3f;
    [SerializeField] private float rotationSpeed = 120f;
    [SerializeField] private int keyValue = 1;

    private bool isCollected;

    void Update()
    {
        if (!isCollected)
        {
            // Xoay chuẩn 2D
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.AddKey(keyValue);
            }

            isCollected = true;
            StartCoroutine(CollectAnimation());
        }
    }

    IEnumerator CollectAnimation()
    {
        Vector3 startScale = transform.localScale;
        float t = 0f;

        while (t < shrinkDuration)
        {
            t += Time.deltaTime;
            float lerp = t / shrinkDuration;

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, lerp);
            transform.position += Vector3.up * Time.deltaTime * 1.5f;

            yield return null;
        }

        Destroy(gameObject);
    }
}
