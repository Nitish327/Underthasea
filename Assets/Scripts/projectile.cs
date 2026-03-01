using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowSpawn;
    public int ammo;
    public Text ammoText;
    public Animator bowAnim;
    public string shootAnimationName;
    public AudioSource bowSound;

    public DisableArmsEnableGun arms;

  


    void Update()
    {
        ammoText.text = ammo.ToString();
        if(Input.GetMouseButtonDown(0))
        {
            if (ammo > 0 && arms.showArms == false)
            {
                bowAnim.Play(shootAnimationName);
                Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
                bowSound.Play();
                ammo--;
            
            }
        }
    }





}
