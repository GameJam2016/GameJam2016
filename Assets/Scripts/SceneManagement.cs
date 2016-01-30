using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    /// <summary>
    /// Loads the scene using the int that represents it in the build
    /// </summary>
    /// <param name="sceneNum">Number of the scene</param>
    public static void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    /// <summary>
    /// Loads the scene using the name of the scene in the build
    /// </summary>
    /// <param name="sceneName">scene name</param>
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
