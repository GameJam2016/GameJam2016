using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScreen : MonoBehaviour 
{
    public Text myText;

    public float flashSpeed = 10.0f;
	
	// Update is called once per frame
	void Update ()
    {
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, Mathf.Sin((Time.time * flashSpeed) % 255));

        if(Input.anyKeyDown)
        {
            SceneManagement.LoadSceneAdditive(0);
            Destroy(gameObject);
        }
	
	}
}
