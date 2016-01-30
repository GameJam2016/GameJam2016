using UnityEngine;
using System.Collections;

public class PlayerFunctionality : MonoBehaviour
{
    public float xMoveSpeed;
    public float yJumpForce;
    private Rigidbody2D thisRigidBody;
    public bool bIsGrounded;
    [SerializeField] float groundCheckRange = 0.5f;
	
    // Use this for initialization
    public void PlayerInitialize()
    {
        thisRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    //movement
    public void MoveLeft()
    {
        thisRigidBody.velocity = new Vector2(-xMoveSpeed, thisRigidBody.velocity.y);
    }

    public void MoveRight()
    {
        thisRigidBody.velocity = new Vector2(xMoveSpeed, thisRigidBody.velocity.y);
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
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, -Vector2.up, groundCheckRange, ~(1 << LayerMask.NameToLayer("Player")));

        Debug.Log(hit.collider.name);
        if(hit.collider.tag != "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
