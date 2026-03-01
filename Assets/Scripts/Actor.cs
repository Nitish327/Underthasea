using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public int arrowDamage;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        // Death function
        // TEMPORARY: Destroy Object
        Destroy(gameObject);
    }
      void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "arrow")
        {
            TakeDamage(arrowDamage);
        }
    }

}
