using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Feedback")]
    public Image damageOverlay;
    public Slider healthBar; // <--- ADD THIS
    public float flashDuration = 0.2f;

    void Start()
    {
        currentHealth = maxHealth;
        
        // Initialize the Health Bar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }

        if (damageOverlay != null)
        {
            var color = damageOverlay.color;
            color.a = 0;
            damageOverlay.color = color;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        
        // Update the Health Bar UI
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (damageOverlay != null) 
        {
            StopAllCoroutines();
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0) Die();
    }

    IEnumerator FlashRed()
    {
        damageOverlay.color = new Color(1, 0, 0, 0.4f); 
        yield return new WaitForSeconds(flashDuration);
        damageOverlay.color = new Color(1, 0, 0, 0f);
    }

    void Die()
    {
        Debug.Log("Player Died!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}