using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisableArmsEnableGun : MonoBehaviour
{

    public GameObject arms;  
    public GameObject bow; 
    public GameObject sword;
    public GameObject axe;
    public GameObject hammer;
    public GameObject spear;

    public KeyCode hideKey = KeyCode.E; 

    public KeyCode switchKey = KeyCode.Q;

    public bool showArms = true;

    public bool unlockSpear = false;

    public bool unlockedHammer = false;

    public string weapon = "sword";

    void Start()
    {
    showArms = arms.activeSelf;
    bow.SetActive(false);
    spear.SetActive(false);
    hammer.SetActive(false);
    axe.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            arms.SetActive(!showArms);
            bow.SetActive(showArms);  
            showArms = !showArms;
        }
        if (Input.GetKeyDown(switchKey) && showArms == true)
        {
            if (weapon == "hammer")
            {
                weapon = "sword";
                hammer.SetActive(false);
                sword.SetActive(true);

            }
            else if (weapon == "sword")
            {
                weapon = "axe";
                sword.SetActive(false);
                axe.SetActive(true);
            } else if (weapon == "axe")
            {
                if (unlockSpear == true)
                {
                    weapon = "spear";
                    axe.SetActive(false);
                    spear.SetActive(true);
                }
                else
                {
                    weapon = "sword";
                    axe.SetActive(false);
                    sword.SetActive(true);
                }
            } else if (weapon == "spear")
            {
                if(unlockedHammer == true)
                {
                    weapon = "hammer";
                    spear.SetActive(false);
                    hammer.SetActive(true);
                }
                else
                {
                    weapon = "sword";
                    spear.SetActive(false);
                    sword.SetActive(true);
                }
            }
            
        }
    }
    
}
