using UnityEngine;
using System.Collections;

public class PlayerInput : PlayerFunctionality
{

	// Use this for initialization
	void Start ()
    {
        PlayerInitialize();
        bIsGrounded = Grounded();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("right"))
        {
            MoveRight();
        }

        else if(Input.GetKey("left"))
        {
            MoveLeft();
        }

        if(Input.GetKey("space"))
        {
            Jump();
        }
    }

}
