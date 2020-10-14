using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingScreen : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            gameObject.SetActive(false);
        SceneManager.sceneLoaded += DisableOnMenu;
    }

    void DisableOnMenu(Scene _scene, LoadSceneMode _loadSceneMode)
    {
        if (_scene.buildIndex == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
