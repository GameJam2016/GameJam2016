using UnityEngine;
using System.Collections;

public class LoadLevels : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D ColliderStay)
    {
        if(ColliderStay.gameObject.tag == "Tutorial" && InputManager.Instance.GetKeyDown("EnterWorld"))
        {
            SceneManagement.LoadScene(1);
        }

        if (ColliderStay.gameObject.tag == "Level1" && InputManager.Instance.GetKeyDown("EnterWorld"))
        {
            SceneManagement.LoadScene(2);
        }

        if (ColliderStay.gameObject.tag == "Level2" && InputManager.Instance.GetKeyDown("EnterWorld"))
        {
            SceneManagement.LoadScene(3);
        }

        if (ColliderStay.gameObject.tag == "ReturnToLevelSelect" && InputManager.Instance.GetKeyDown("EnterWorld"))
        {
            SceneManagement.LoadScene(0);
        }
    }
}
