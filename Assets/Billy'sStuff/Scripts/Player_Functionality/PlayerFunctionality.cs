using UnityEngine;
using System.Collections;

public class PlayerFunctionality : MonoBehaviour
{
    public float xMoveSpeed;
    public float yJumpForce;
    private Rigidbody2D thisRigidBody;
    public bool bIsGrounded, bIsOnLadder;
    [SerializeField] float groundCheckRange = 0.5f;
	

    // Use this for initialization
    public void PlayerInitialize()
    {
        //setting variable to the game's rigidbody2D
        thisRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    //movement
    public void MoveLeft()
    {
        //if(!bIsOnLadder)
        //{
            thisRigidBody.velocity = new Vector2(-xMoveSpeed, thisRigidBody.velocity.y);
        //}     
    }

    public void MoveRight()
    {
        //if(!bIsOnLadder)
        //{
            thisRigidBody.velocity = new Vector2(xMoveSpeed, thisRigidBody.velocity.y);
        //}
    }

  
    //able to move up on vine/ladder
    public void MoveUp()
    {
        if(bIsOnLadder)
        {
            thisRigidBody.velocity = new Vector2(0, xMoveSpeed);
        }
    }

    //able to move down on vine/ladder
    public void MoveDown()
    {
        if (bIsOnLadder)
        {
            thisRigidBody.velocity = new Vector2(0, -xMoveSpeed);
        }
    }

    //sets the velocity of the player to zero
    public void MovementCancel()
    {
        if(Grounded() || bIsOnLadder)
        {
            Debug.Log("cancel");
            thisRigidBody.velocity = Vector2.zero;
        }
        
    }

    public void Jump()
    {
        if(bIsGrounded)
        {
            thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, yJumpForce);
        }
    } 

    //grounded Check
    public bool Grounded()
    {
        //when raycast hits another object that isn't player it will return true, otherwise false.
        return Physics2D.Raycast(gameObject.transform.position, -Vector2.up, groundCheckRange, ~(1 << LayerMask.NameToLayer("Player")));
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //entering ladder
        if(other.tag == "Ladder")
        {
            bIsOnLadder = true;
            //setting gravity to zero so player can "climb" up ladder/vine
            thisRigidBody.gravityScale = 0;
            //zero the velocity on first contact with ladder/vine
            thisRigidBody.velocity = new Vector2(0, 0);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //leaving ladder
        if(other.tag == "Ladder")
        {
            bIsOnLadder = false;
            thisRigidBody.gravityScale = 1;
        }
    }
}
