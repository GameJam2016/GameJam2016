using UnityEngine;
using System.Collections;

public class Golem : Enemy
{
    public bool alerted, moving, attacking;
    private Vector2 leftDirection, rightDirection;

    public GameObject attackCollider;
    public bool patrolLeft;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
        attackCollider.GetComponent<CircleCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {

        alerted = alert();

        // We attacked, so no other actions.
        if (attacked)
        {

        }

        // Attack time.
        else if (alerted && killTime())
        {
            StartCoroutine(attack());
            StartCoroutine(attackCooldown(attackSpeed));
        }

        else if (alerted )
        {
            moving = false;
            chase();
        }

        else
        {
            patrol();
        }
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player" && attackCollider.GetComponent<CircleCollider2D>().enabled)
        {
            DamageManager.Instance.SendDamage(this.gameObject, other.gameObject.GetComponent<DamageableObject>(), MyAttribute, damage, false);
        }
    }

    public bool killTime()
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

    // This will tell us if a player has come into our domain.
    bool alert ()
    {
        
        if (leftBound.transform.position.x < player.transform.position.x && rightBound.transform.position.x > player.transform.position.x && this.transform.position.y + 5.0f >= player.transform.position.y)
        {
            return true;
        }

        else
        {
            return false;
        }
    }


    // This function will cause the Golem to Patrol.
    void patrol()
    {
        if (patrolLeft)
        {
            // move left.
            if (!moving)
            {
                StartCoroutine(move(leftBound.transform.position));
            }
            
            // If we've reached left point.
            if ((leftBound.transform.position - this.transform.position).magnitude < 2)
            {
                patrolLeft = false;
                moving = false;
            }
        }

        if (!patrolLeft)
        {
            // move right.
            if (!moving)
            {
                StartCoroutine(move(rightBound.transform.position));
            }

            if ((rightBound.transform.position - this.transform.position).magnitude < 2)
            {
                patrolLeft = true;
                moving = false;
            }
        }
    }

    // This function will cause us to chase the player when they are between our left and right bounds, 
    // and 5 or lower units above us.
    void chase ()
    {
        Vector3 chaseTarget = new Vector3(player.transform.position.x - this.transform.position.x, 0, 0);

        myRigid.AddForce((chaseTarget).normalized * moveSpeed / 2);

        // If we have gone past the left or right bounds, we are immediately translated to them.
        if (this.transform.position.x > rightBound.transform.position.x + 0.5)
        {
            this.transform.position = rightBound.transform.position;
            myRigid.velocity = new Vector2();
            patrolLeft = true;
            moving = false;
        }

        if (this.transform.position.x < leftBound.transform.position.x - 0.5)
        {
            this.transform.position = leftBound.transform.position;
            myRigid.velocity = new Vector2();
            patrolLeft = false;
            moving = false;
        }
    }

    // This coroutine will cause us to attack after a small delay.
    IEnumerator attack ()
    {
        myRigid.velocity = new Vector2();
        yield return new WaitForSeconds(attackSpeed/2);
        attackCollider.GetComponent<CircleCollider2D>().enabled = true;
        yield return null;
        attackCollider.GetComponent<CircleCollider2D>().enabled = false;
    }

    // This coroutine will apply a force until we reach our target.
    IEnumerator move(Vector2 target)
    {
        Vector3 moveTarget = new Vector3(target.x, target.y, 0);
        // We're moving, so don't try to move again.
        moving = true;
        do
        {
            
            // If we have gone past the left or right bounds, we are immediately translated to them.
            if (this.transform.position.x > rightBound.transform.position.x)
            {
                this.transform.position = rightBound.transform.position;
            }

            if (this.transform.position.x < leftBound.transform.position.x)
            {
                this.transform.position = rightBound.transform.position;
            }

            // If we're not going faster than our moveSpeed, add some force to fix that.
            if (myRigid.velocity.magnitude < moveSpeed)
            {
                // We add force equal to our moveSpeed divided by half in the direction of the target.
                myRigid.AddForce((moveTarget - this.transform.position).normalized * moveSpeed / 2);
            }

            yield return null;

            // Keep doing this until we get within 2 units of our target, or our moving status is changed.
            // For example, when we see that player.
        } while ((moveTarget - this.transform.position).magnitude >= 2 && moving);
        moving = false;

        // Set velocity to 0 so we can quickly change direction.
        myRigid.velocity = new Vector2();
    }
}
