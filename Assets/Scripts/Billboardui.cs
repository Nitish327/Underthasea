using UnityEngine;
using UnityEngine.UI;

public class BillboardUI : MonoBehaviour
{
    public Actor enemy;
    public Slider healthBar;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;

        if (enemy == null)
        {
            Debug.LogError("Enemy not assigned!");
            return;
        }

        if (healthBar == null)
        {
            Debug.LogError("HealthBar not assigned!");
            return;
        }

        healthBar.maxValue = enemy.maxHealth;
        healthBar.value = enemy.currentHealth;
    }

    void Update()
    {
        LookAtPlayer();
        UpdateHealthSlider();
    }

    void LookAtPlayer()
    {
        transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                         playerCamera.transform.rotation * Vector3.up);
    }

    void UpdateHealthSlider()
    {
        if (enemy != null && healthBar != null)
        {
            healthBar.value = enemy.currentHealth;
        }
    }
}