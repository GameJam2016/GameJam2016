using UnityEngine;
using System.Collections;

public class Wisp : Enemy
{
    // Whether we have seen the player.
    private bool alerted;
    
    // The spawner location.
    [HideInInspector] public GameObject spawner;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        alerted = seePlayer();
	}

    // This is used to determine if we see the player. If so, we chase him down.
    bool seePlayer ()
    {
        GameObject player = GameObject.Find("Player");
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, (player.transform.position - this.transform.position));

        // If we've seen the player
        if (hit.collider.gameObject.tag == "Player")
        {
            return true;
        }

        // We didn't
        else
        {
            return false;
        }
    }

    // This move function will cause the Wisp to meander near where it spawned.
    void meander ()
    {

    }
}
