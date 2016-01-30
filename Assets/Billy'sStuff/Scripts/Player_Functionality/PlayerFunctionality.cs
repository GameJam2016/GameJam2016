using UnityEngine;
using System.Collections;

public class PlayerFunctionality : MonoBehaviour
{
    public float xMoveSpeed;
    public float yJumpForce;
    private Rigidbody2D thisRigidBody;
    public bool bIsGrounded;
    public bool bOnLadder = false;
    [SerializeField] float groundCheckRange = 1.0f;
	

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
    
    public void Stop()
    {
        thisRigidBody.velocity = new Vector2(0, thisRigidBody.velocity.y);
    }

    public void Jump()
    {
        if(bIsGrounded)
        {
            bOnLadder = false;
            thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, yJumpForce);
        }
    } 

    public void CastSpell()
    {
        if(!GetComponent<PlayerStatus>().MySpells[0])
        {
            return;
        }
        Instantiate(GetComponent<PlayerStatus>().MySpells[0], transform.position, transform.rotation);

    }

    public void MoveUpLadder()
    {
        if (!bOnLadder)
            return;
        
        thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, xMoveSpeed);
    }

    public void MoveDownLadder()
    {
        if (!bOnLadder)
            return;

        thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, -xMoveSpeed);
    }

    public void StopLadderMove()
    {
        if (!bOnLadder)
            return;

        thisRigidBody.velocity = new Vector2(0, 0);
    }
    //grounded Check
    public bool Grounded()
    {
        // RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, -Vector2.up, groundCheckRange, ~(1 << LayerMask.NameToLayer("Player")));

        Debug.DrawRay(transform.position, -Vector2.up, Color.red, 1.0f);

        return Physics2D.Raycast(gameObject.transform.position, -Vector2.up, groundCheckRange, ~(1 << LayerMask.NameToLayer("Player")));

        //Debug.Log("name: " + hit.collider.name);
        //if(hit.collider.tag != "Player")
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Ladder" && Input.GetAxis("Vertical") != 0)
        {
            transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
            bOnLadder = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            bOnLadder = false;
        }
    }

}
