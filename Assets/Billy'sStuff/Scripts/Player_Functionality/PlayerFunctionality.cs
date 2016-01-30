﻿using UnityEngine;
using System.Collections;

public class PlayerFunctionality : MonoBehaviour
{
    public float xMoveSpeed;
    public float yJumpForce;
    private Rigidbody2D thisRigidBody;
    public bool bIsGrounded;
    public bool bOnLadder = false;

    private int currentSpell = 0;
    public bool bHasJumped = false;
    public bool bIsAttacking = false;
    [SerializeField] float groundCheckRange = 1.0f;
	

    // Use this for initialization
    public void PlayerInitialize()
    {
        thisRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    //movement
    public void MoveLeft()
    {
        if(bIsAttacking)
        {
            Stop();
            return;
        }
        transform.localScale = new Vector3(-1, 1, 1);
        thisRigidBody.velocity = new Vector2(-xMoveSpeed, thisRigidBody.velocity.y);
    }

    public void MoveRight()
    {
        if (bIsAttacking)
        {
            Stop();
            return;
        }
        transform.localScale = new Vector3(1, 1, 1);
        thisRigidBody.velocity = new Vector2(xMoveSpeed, thisRigidBody.velocity.y);
    }
    
    public void Stop()
    {
        thisRigidBody.velocity = new Vector2(0, thisRigidBody.velocity.y);
    }

    public void Jump()
    {
        if(bIsGrounded || bOnLadder)
        {
            bOnLadder = false;
            StartCoroutine(COJumped());
            thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, yJumpForce);
        }
    } 

    public void CastSpell()
    {
        if(!GetComponent<PlayerStatus>().MySpells[currentSpell])
        {
            return;
        }
        Instantiate(GetComponent<PlayerStatus>().MySpells[currentSpell], transform.position, transform.rotation);

    }

    public void ChangeSpellRight()
    {
        currentSpell++;
        if (currentSpell > 14 || !GetComponent<PlayerStatus>().MySpells[currentSpell])
        {
            currentSpell = 0;
        }
    }

    public void ChangeSpellLeft()
    {
        currentSpell--;
        if(currentSpell < 0)
        {
            currentSpell = 14;
        }
        while(!GetComponent<PlayerStatus>().MySpells[currentSpell])
        {
            currentSpell--;
        }
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
            if (bHasJumped)
            {
                return;
            }
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

    IEnumerator COJumped()
    {
        bHasJumped = true;
        yield return new WaitForSeconds(0.5f);
        bHasJumped = false;
    }
}
