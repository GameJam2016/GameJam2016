using UnityEngine;
using System.Collections;

public class Wisp : Enemy
{
    // Whether we have seen the player.
    private bool alerted, moving;
    private GameObject player;
    
    // The spawner location.
    public GameObject spawner;
    public float chaseSpeed;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
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

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player" && !stunned)
        {
            DamageManager.Instance.SendDamage(this.gameObject.GetComponent<DamageableObject>(), other.gameObject.GetComponent<DamageableObject>(), MyAttribute, damage, false);
        }
    }

    // This is used to determine if we see the player. If so, we chase him down.
    bool seePlayer ()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, (player.transform.position - this.transform.position));
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            moveSpeed = chaseSpeed;
            moving = false;
            return true;
        }

        // We didn't
        else
        {
            return false;
        }
    }

    // This function will tell us if we're within range to attack, we're not stunned, and the player is not invisible.
    // At which point, we attack.
    bool killTime ()
    {
        if ((this.transform.position - player.transform.position).magnitude <= 2 && !stunned && !attacked)
        {
            return true;
        }

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
        x = Random.Range(-1.0f, 1.0f);
        y = Random.Range(-1.0f, 1.0f);

        moveLocation = new Vector2(x * leash + spawner.transform.position.x, y * leash + spawner.transform.position.y);
        
        if (!moving)
        {
            StartCoroutine(move(moveLocation));
        }
    }

    // This function will cause the Wisp to chase the player.
    void chase ()
    {
        if (killTime())
        {
            moving = false;
            attack();
        }

        else if (!moving)
        {
            Vector2 moveTarget = player.transform.position;
            StartCoroutine(move(moveTarget));
        }
    }

    // We shall attack.
    void attack ()
    {
        Vector2 dashDirection = new Vector2((player.transform.position - this.transform.position).x, (player.transform.position - this.transform.position).y);
        
        StartCoroutine(attackCooldown(attackSpeed));
        StartCoroutine(dash(dashDirection));
    }

    // This function will apply a force until we reach our target.
    IEnumerator move (Vector2 target)
    {
        Vector3 moveTarget = new Vector3(target.x, target.y, 0);
        moving = true;
        do
        {
            if (myRigid.velocity.magnitude < moveSpeed)
            {
                myRigid.AddForce((moveTarget - this.transform.position).normalized * moveSpeed/2);
            }

            yield return null;
        } while ((moveTarget - this.transform.position).magnitude >= 2 && moving);
        moving = false;
        myRigid.velocity = new Vector2();
    }

    // This function will cause us to dash at double chase speed into the player.
    IEnumerator dash (Vector2 target)
    {
        do
        {
            myRigid.velocity = target.normalized * chaseSpeed * 2;
            yield return null;
        } while (attacked);
        myRigid.velocity = new Vector2();
    }
}
