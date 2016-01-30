﻿using UnityEngine;
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
            transform.localScale = new Vector3(1, 1, 1);
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

        if (Input.GetAxis("Vertical") > 0)
        {
            MoveUpLadder();
        }

        else if (Input.GetAxis("Vertical") < 0)
        {
            MoveDownLadder();
        }
        else
        {
            StopLadderMove();
        }

        if (InputManager.Instance.GetKey("Jump") || Input.GetKey("space"))
        {
            Jump();
        }

        if(InputManager.Instance.GetKey("Attack"))
        {
            if (!bIsAttacking)
            {
                GetComponent<PlayerStatus>().myAnimator.SetBool("Attacking", true);
            }
        }

        if(InputManager.Instance.GetKey("Parry"))
        {
            Debug.Log("parry");
        }

        if(InputManager.Instance.GetKeyDown("Cast"))
        {
            CastSpell();
        }

        if (InputManager.Instance.GetKeyDown("LeftBumper"))
        {
            ChangeSpellLeft();
        }
        if (InputManager.Instance.GetKey("RightBumper"))
        {
            ChangeSpellRight();
        }

        bIsAttacking = GetComponent<PlayerStatus>().myAnimator.GetBool("Attacking");
        GetComponent<PlayerStatus>().myAnimator.SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
    }

}
