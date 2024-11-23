using UnityEngine;

public class BurgerProjectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float damage = 25f; // Damage dealt to enemies
    [SerializeField] private GameObject splatterEffect; // Effect on impact
    [SerializeField] private float rotationSpeed = 720f; // Spin speed

    private void Update()
    {
        // Make burger spin while flying
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we hit an enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Apply damage
            Debug.Log($"Hit {enemy.name}, dealt {damage} damage.");
        }

        // Spawn splatter effect at collision point
        if (splatterEffect != null)
        {
            Instantiate(splatterEffect, transform.position, Quaternion.identity);
        }

        // Destroy the projectile
        Destroy(gameObject);
    }
}
