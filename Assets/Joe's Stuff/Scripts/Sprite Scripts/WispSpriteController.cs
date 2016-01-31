using UnityEngine;
using System.Collections;

public class WispSpriteController : MonoBehaviour
{
    public Wisp AI;
    public Animator controller;
    public GameObject aimer, attack;

    private Quaternion looking;

    // Use this for initialization
    void Start ()
    {
        attack.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (AI.alerted)
        {
            controller.SetBool("Idle", false);
        }

        if (AI.attacked)
        {
            looking.SetLookRotation(AI.myRigid.velocity * 2 + new Vector2( this.transform.position.x, this.transform.position.y));
            aimer.transform.rotation = looking;
            attack.GetComponent<SpriteRenderer>().enabled = true;
            controller.GetComponent<SpriteRenderer>().enabled = false;
        }

        else
        {
            looking.SetLookRotation(Vector3.up);
            aimer.transform.rotation = looking;
            attack.GetComponent<SpriteRenderer>().enabled = false;
            controller.GetComponent<SpriteRenderer>().enabled = true;
        }
	}
}
