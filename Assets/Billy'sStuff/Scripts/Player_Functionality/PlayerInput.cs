using UnityEngine;
using System.Collections;

public class PlayerInput : PlayerFunctionality
{

	// Use this for initialization
	void Start ()
    {
        //initializing from playerFunctionality script
        PlayerInitialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        bIsGrounded = Grounded();

        //moving right
        if( InputManager.Instance.GetKey("Right") || Input.GetKey("right"))
        {
            MoveRight();
        }

        //moving left
        else if ( InputManager.Instance.GetKey("Left") || Input.GetKey("left"))
        {
            MoveLeft();
        }
        
        //moving up
        else if(InputManager.Instance.GetKey("Up") || Input.GetKey("up"))
        {
            MoveUp();
        }

        //moving down
        else if(InputManager.Instance.GetKey("Down") || Input.GetKey("down"))
        {
            MoveDown();
        }

        //when none of the keys are pressed cancel the velocity to prevent sliding
        else
        {
            MovementCancel();
        }

        //jumpng
        if(InputManager.Instance.GetKeyDown("Jump") || Input.GetKeyDown("space"))
        {
            Jump();
        }
    }

}
