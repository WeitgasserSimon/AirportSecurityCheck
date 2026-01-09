using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Name der Scene, zu der gewechselt werden soll
    public string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
