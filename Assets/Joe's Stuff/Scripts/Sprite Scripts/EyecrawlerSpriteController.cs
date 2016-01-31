using UnityEngine;
using System.Collections;

public class EyecrawlerSpriteController : MonoBehaviour
{
    public Animator controller;
    public EyeCrawler AI;

    private bool attacking;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
        if (attacking)
        {
            attacking = false;
        }


        else if (AI.fleeing && AI.myRigid.velocity.magnitude > 0.2)
        {
            controller.SetBool("Stationary", false);

            if (AI.patrolLeft)
            {
                this.transform.localScale = new Vector3(-2, this.transform.localScale.y, this.transform.localScale.z);
            }

            else
            {
                this.transform.localScale = new Vector3(2, this.transform.localScale.y, this.transform.localScale.z);
            }

        }

        else if (AI.myRigid.velocity.magnitude > 0.2)
        {
            controller.SetBool("Stationary", false);

            if (AI.patrolLeft)
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
            controller.SetBool("Stationary", true);
            if (AI.patrolLeft)
            {
                this.transform.localScale = new Vector3(-2, this.transform.localScale.y, this.transform.localScale.z);
            }

            else
            {
                this.transform.localScale = new Vector3(2, this.transform.localScale.y, this.transform.localScale.z);
            }
        }
	}

    public void attack ()
    {
        attacking = true;
        controller.SetTrigger("Attack");
    }
}
