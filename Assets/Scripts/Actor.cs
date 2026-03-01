using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Actor : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public int arrowDamage;

    public string sceneToLoad = "";

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

        if(maxHealth == 200)
        {
            SceneManager.LoadScene(sceneToLoad);
        }else{
            Destroy(this.gameObject);}
       
        
    }
      void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "arrow")
        {
            TakeDamage(arrowDamage);
        }
    }

}
