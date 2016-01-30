using UnityEngine;
using System.Collections;

public class Wisp : Enemy
{
    // Whether we have seen the player.
    private bool alerted, moving;
    private GameObject player;
    
    // The spawner location.
    [HideInInspector] public GameObject spawner;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!alerted)
        {
            alerted = seePlayer();
        }

        if (!moving && !alerted)
        {
            meander();
        }

        else if (alerted)
        {
            chase();
        }
	}

    // This is used to determine if we see the player. If so, we chase him down.
    bool seePlayer ()
    {
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

    // This function will cause the Wisp to meander near where it spawned.
    void meander ()
    {
        float leash = spawner.GetComponent<EnemySpawner>().range;
        Vector2 moveLocation;
        float x, y = 0;
        x = Random.Range(0, 1);
        y = Random.Range(0, 1);

        moveLocation = new Vector2(x * leash, y * leash);
        if (!moving)
        {
            StartCoroutine(move(moveLocation));
        }
    }

    // This function will cause the Wisp to chase the player.
    void chase ()
    {
        Vector2 moveTarget = player.transform.position - this.transform.position;
        if (!moving)
        {
            StartCoroutine(move(moveTarget));
        }
    }

    IEnumerator move (Vector2 target)
    {
        Vector3 moveTarget = new Vector3(target.x, target.y, 0);
        moving = true;
        do
        {
            if (myRigid.velocity.magnitude < moveSpeed)
            {
                myRigid.AddForce((moveTarget - this.transform.position).normalized * moveSpeed);
            }

            yield return null;
        } while ((moveTarget - this.transform.position).magnitude >= 1);
        moving = false;
    }
}
