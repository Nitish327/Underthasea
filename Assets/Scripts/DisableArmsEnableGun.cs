using UnityEngine;

public class DisableArmsEnableGun : MonoBehaviour
{

    public GameObject arms;  
    public GameObject bow;
    public KeyCode hideKey = KeyCode.E; 

    public bool showArms = true;

    void Start()
    {
    showArms = arms.activeSelf;
    bow.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            arms.SetActive(!showArms);
            bow.SetActive(showArms); // 
            showArms = !showArms;
        }
    }
}
