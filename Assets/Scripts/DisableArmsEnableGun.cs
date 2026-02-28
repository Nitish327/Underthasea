using UnityEngine;

public class DisableArmsEnableGun : MonoBehaviour
{

    public GameObject arms;  
    public KeyCode hideKey = KeyCode.E; 

    public bool showArms = true;
    void Start()
    {
    showArms = arms.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            arms.SetActive(!showArms); // 
            showArms = !showArms;
        }
    }
}
