using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowSpawn;
    public int ammo = 10;
    public Text ammoText; // Legacy UI Text
    public Animator bowAnim;
    public string shootAnimationName;
    public AudioSource bowSound;

    public DisableArmsEnableGun arms;

    void Start()
    {
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0 && arms.showArms == false)
            {
                ShootArrow();
            }
        }
    }

    void ShootArrow()
    {
        ammo--;
        bowAnim.Play(shootAnimationName);
        Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        UpdateAmmoUI();
        Debug.Log("Ammo: " + ammo);
        bowSound.Play();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = ammo.ToString();
        else
            Debug.LogWarning("Ammo Text is not assigned in the Inspector!");
    }
}