using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public int WinCount = 10;

    public GameObject WinCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Win()
    {
        
        //Enable win
        WinCanvas.SetActive(true);
        SceneManagement.LoadScene(0);
    }
}
