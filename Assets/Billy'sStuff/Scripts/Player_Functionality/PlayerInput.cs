using UnityEngine;
using System.Collections;

public class PlayerInput : PlayerFunctionality
{
    public GameObject SpellManagerObject;
	// Use this for initialization
	void Start ()
    {
        PlayerInitialize();
        SpellManagerObject = GameObject.FindGameObjectWithTag("SpellManagaer");
	}
	
	// Update is called once per frame
	void Update ()
    {
        bIsGrounded = Grounded();
        if(SpellManagerObject.GetComponent<SpellManager>().InventoryOpen == false)
        {
            if (InputManager.Instance.GetKey("Right") || Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                MoveRight();
            }

            else if (InputManager.Instance.GetKey("Left") || Input.GetAxis("Horizontal") < 0)
            {
                MoveLeft();
            }
            else
            {
                Stop();
            }

            if (InputManager.Instance.GetKey("Jump") || Input.GetKey("space"))
            {
                Jump();
            }
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

       
        if(InputManager.Instance.GetKey("Attack") && Application.loadedLevelName != "LevelSelect")
        {
            if (!bIsAttacking)
            {
                GetComponent<PlayerStatus>().myAnimator.SetBool("Attacking", true);
            }
        }

        //if(InputManager.Instance.GetKey("Parry"))
        //{
        //    Debug.Log("parry");
        //}

        if(InputManager.Instance.GetKeyDown("Cast"))
        {
            CastSpell();
        }

        bIsAttacking = GetComponent<PlayerStatus>().myAnimator.GetBool("Attacking");
        GetComponent<PlayerStatus>().myAnimator.SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
    }

}
