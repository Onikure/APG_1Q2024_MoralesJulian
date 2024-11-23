using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Visual Feedback")]
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float hitFlashDuration = 0.15f;
    [SerializeField] private GameObject deathEffect;

    [Header("Ragdoll Settings")]
    [SerializeField] private float destroyDelay = 3f;
    [SerializeField] private Animator animator;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;

    private Renderer enemyRenderer;
    public bool IsDead { get; private set; }

    [SerializeField] private float playerDamage = 10f;

    [Header("Burger Drop Settings")]
    [SerializeField] private GameObject burgerPrefab; // Add reference to burger prefab
    [SerializeField] private float burgerDropChance = 0.5f; // Chance to drop a burger (0 = never, 1 = always)

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }
    }

    void Awake()
    {
        currentHealth = maxHealth;
        enemyRenderer = GetComponentInChildren<Renderer>();
        IsDead = false;

        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        SetRagdollState(false);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        StartCoroutine(FlashDamage());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashDamage()
    {
        if (enemyRenderer != null && hitMaterial != null)
        {
            Material originalMaterial = enemyRenderer.material;
            enemyRenderer.material = hitMaterial;
            yield return new WaitForSeconds(hitFlashDuration);
            enemyRenderer.material = originalMaterial;
        }
    }

    private void SetRagdollState(bool state)
    {
        if (animator != null)
            animator.enabled = !state;

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !state;
            rb.useGravity = state;
        }

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = state;
        }
    }

    private void Die()
    {
        IsDead = true;

        // Drop burger with a certain chance
        DropBurger();

        SetRagdollState(true);

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.AddForce(Random.insideUnitSphere * 5f, ForceMode.Impulse);
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        StartCoroutine(DestroyAfterDelay());
    }

    private void DropBurger()
    {
      
        if (burgerPrefab != null && Random.value <= burgerDropChance)
        {

            Instantiate(burgerPrefab, transform.position, Quaternion.identity);
            Debug.Log("Burger dropped!");
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    void OnGUI()
    {
        if (Camera.main == null || IsDead) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.z > 0)
        {
            float healthPercentage = (currentHealth / maxHealth) * 100;
            GUI.Label(new Rect(screenPos.x - 25, Screen.height - screenPos.y - 30, 100, 20), $"HP: {healthPercentage:0}%");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerDamage);
                Debug.Log("Enemy hit the player! Dealt " + playerDamage + " damage.");
            }
        }
    }
}
