using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private GameObject deathEffect; // Optional for visual effects

    void Start()
    {
        currentHealth = maxHealth; // Initialize the health
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Print debug message
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Show death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Reload the Home scene or whatever you want to happen after death
        Debug.Log("Player Died");
        SceneManager.LoadScene("HomeScene"); // Replace "HomeScene" with the actual name of your home scene
    }
}
