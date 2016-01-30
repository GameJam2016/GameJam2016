using UnityEngine;
using System.Collections;

public class Wisp : Enemy
{
    // Whether we have seen the player, whether we are in the process of moving.
    private bool alerted, moving;
    
    
    // The spawner location.
    public GameObject spawner;
    // The speed at which we chase and dash.
    public float chaseSpeed;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // If we haven't already seen the player, we check to see if we see the player.
        if (!alerted)
        {
            alerted = seePlayer();
        }

        // If we aren't already moving and we haven't seen the player, we meander.
        if (!moving && !alerted)
        {
            meander();
        }

        // If we've seen the player, we chase them.
        else if (alerted)
        {
            chase();
        }
	}

    // If we collide with another player, and we're not stunned, we damage them.
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !stunned)
        {
            DamageManager.Instance.SendDamage(this.gameObject, other.gameObject.GetComponent<DamageableObject>(), MyAttribute, damage, false);
        }
    }

    // This is used to determine if we see the player. If so, we chase him down.
    bool seePlayer ()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, (player.transform.position - this.transform.position));
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            // Since we're chasing, we're faster.
            moveSpeed = chaseSpeed;

            // We cancel our last move and start a new one.
            moving = false;
            return true;
        }

        // We didn't
        else
        {
            return false;
        }
    }

    // This function will tell us if we're within range to attack, we're not stunned, we haven't attacked, and the 
    // player is not invisible. At which point, we attack.
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
        // The distance it can meander from the original spawner.
        float leash = spawner.GetComponent<EnemySpawner>().range;
        Vector2 moveLocation;
        float x, y = 0;
        x = Random.Range(-1.0f, 1.0f);
        y = Random.Range(-1.0f, 1.0f);

        // A random location within leash distance of the spawner.
        moveLocation = new Vector2(x * leash + spawner.transform.position.x, y * leash + spawner.transform.position.y);
        
        // If we aren't already moving, we move towards the new location.
        if (!moving)
        {
            StartCoroutine(move(moveLocation));
        }
    }

    // This function will cause the Wisp to chase the player.
    void chase ()
    {
        // Check if we're within attack range, and we can attack. If so, we stop chasing and dash.
        if (killTime())
        {
            moving = false;
            attack();
        }

        // We constantly move towards the player at chase speed.
        else if (!moving)
        {
            Vector2 moveTarget = player.transform.position;
            StartCoroutine(move(moveTarget));
        }
    }

    // We shall attack.
    void attack ()
    {
        // We're gonna dash in the direction of the player.
        Vector2 dashDirection = new Vector2((player.transform.position - this.transform.position).x, (player.transform.position - this.transform.position).y);
        
        // Our attackcooldown and dash timer are started. Both end when an amount of time larger than attack speed is reached.
        StartCoroutine(attackCooldown(attackSpeed));
        StartCoroutine(dash(dashDirection));
    }

    // This function will apply a force until we reach our target.
    IEnumerator move (Vector2 target)
    {
        Vector3 moveTarget = new Vector3(target.x, target.y, 0);
        // We're moving, so don't try to move again.
        moving = true;
        do
        {
            // If we're not going faster than our moveSpeed, add some force to fix that.
            if (myRigid.velocity.magnitude < moveSpeed)
            {
                // We add force equal to our moveSpeed divided by half in the direction of the target.
                myRigid.AddForce((moveTarget - this.transform.position).normalized * moveSpeed/2);
            }

            yield return null;

            // Keep doing this until we get within 2 units of our target, or our moving status is changed.
            // For example, when we see that player.
        } while ((moveTarget - this.transform.position).magnitude >= 2 && moving);
        moving = false;

        // Set velocity to 0 so we can quickly change direction.
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
