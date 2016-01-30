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

        if( InputManager.Instance.GetKey("Right") || Input.GetAxis("Horizontal") > 0)
        {
            MoveRight();
        }

        else  if( InputManager.Instance.GetKey("Left") || Input.GetAxis("Horizontal") < 0)
        {
            MoveLeft();
        }
        else
        {
            Stop();
        }

        if(InputManager.Instance.GetKey("Jump") || Input.GetKey("space"))
        {
            Jump();
        }

        if(InputManager.Instance.GetKey("Attack"))
        {
            Debug.Log("Attack");
        }

        if(InputManager.Instance.GetKey("Parry"))
        {
            Debug.Log("parry");
        }

        if(InputManager.Instance.GetKeyDown("Cast"))
        {
            CastSpell();
        }
    }

}
