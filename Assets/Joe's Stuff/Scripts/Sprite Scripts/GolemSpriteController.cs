using UnityEngine;
using System.Collections;

public class GolemSpriteController : MonoBehaviour
{
    public Animator controller;
    public Golem AI;

    private bool attacking;
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (checkAttacking())
        {

        }

        else if (AI.alerted)
        {
            if (AI.player.transform.position.x < AI.transform.position.x)
            {
                this.transform.localScale = new Vector3(2, this.transform.localScale.y, this.transform.localScale.z);
            }

            else
            {
                this.transform.localScale = new Vector3(-2, this.transform.localScale.y, this.transform.localScale.z);
            }
        }

        else
        {
            checkStationary();

            if (AI.patrolLeft)
            {
                this.transform.localScale = new Vector3(2, this.transform.localScale.y, this.transform.localScale.z);
            }

            else
            {
                this.transform.localScale = new Vector3(-2, this.transform.localScale.y, this.transform.localScale.z);
            }
        }
	}

    void checkStationary ()
    {
        if (!AI.moving)
        {
            controller.SetBool("Stationary", true);
        }

        else
        {
            controller.SetBool("Stationary", false);
        }
    }

    bool checkAttacking ()
    {
        if (AI.attackCollider.GetComponent<CircleCollider2D>().enabled)
        {
            controller.SetTrigger("Attack");
            return true;
        }
        return false;
    }

    
}
