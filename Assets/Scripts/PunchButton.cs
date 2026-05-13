using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchButton : MonoBehaviour
{
    [SerializeField] string sceneToLoad; 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GloveLeft" || other.gameObject.tag == "GloveRight")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
