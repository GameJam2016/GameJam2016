using UnityEngine;
using System.Collections;

public class PlayerInput : PlayerFunctionality
{

	// Use this for initialization
	void Start ()
    {
        PlayerInitialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        bIsGrounded = Grounded();

        if( InputManager.Instance.GetKey("Right") || Input.GetKeyDown("right"))
        {
            MoveRight();
        }

        else  if( InputManager.Instance.GetKey("Left") || Input.GetKeyDown("left"))
        {
            MoveLeft();
        }

        if(InputManager.Instance.GetKeyDown("Jump") || Input.GetKeyDown("space"))
        {
            Jump();
        }
    }

}
