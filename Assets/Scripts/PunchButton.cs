using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchButton : MonoBehaviour
{
    [SerializeField] string sceneToLoad; 

    // Simple button implementation, if you punch the trigger it loads the chosen scene.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GloveLeft" || other.gameObject.tag == "GloveRight")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
