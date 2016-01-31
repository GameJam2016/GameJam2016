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

        if (this.gameObject.tag == "Level1" && InputManager.Instance.GetKeyDown("EnterWorld"))
        {
            Application.LoadLevel(2);
        }

        if (this.gameObject.tag == "Level2" && InputManager.Instance.GetKeyDown("EnterWorld"))
        {
            Application.LoadLevel(3);
        }

        if (this.gameObject.tag == "Level2" && InputManager.Instance.GetKeyDown("EnterWorld"))
        {
            Application.LoadLevel(0);
        }
    }
}
