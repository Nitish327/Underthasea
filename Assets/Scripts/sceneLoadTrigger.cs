using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTrigger : MonoBehaviour
{
    public string sceneToLoad;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
