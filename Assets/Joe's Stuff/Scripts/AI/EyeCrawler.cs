using UnityEngine;
using System.Collections;

public class EyeCrawler : Enemy
{
    private bool alerted, moving, fleeing;
    private Vector2 leftDirection, rightDirection;

    public GameObject projectile;
    public bool patrolLeft;
    public float launchSpeed;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        alerted = alert();

        if (alerted && killTime())
        {
            attack();
            StartCoroutine(attackCooldown(attackSpeed));
        }

        else if (alerted)
        {
            flee();
        }

        else
        {
            patrol();
        }
    }

    bool killTime()
    {
        if (!stunned && !attacked)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    // This will tell us if a player has come into our domain.
    bool alert()
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
        fleeing = false;
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
    void flee()
    {
        if (!fleeing)
        {
            moving = false;
            fleeing = true;
        }

        
        else if (player.transform.position.x > this.transform.position.x && !moving)
        {
            patrolLeft = true;
            StartCoroutine(move(leftBound.transform.position));
        }

        else if (!moving)
        {
            patrolLeft = false;
            StartCoroutine(move(rightBound.transform.position));
        }

        if (patrolLeft && player.transform.position.x < this.transform.position.x )
        {
            moving = false;
            patrolLeft = false;
        }

        if (!patrolLeft && player.transform.position.x > this.transform.position.x)
        {
            moving = false;
            patrolLeft = true;
        }
    }

    // This function will launch a projectile at the enemy.
    void attack ()
    {
        GameObject proj;
        if (player.transform.position.x > this.transform.position.x)
        {
            proj = Instantiate(projectile, (this.transform.position + transform.up + transform.right), Quaternion.identity) as GameObject;
            proj.GetComponent<EyeCrawlerProjectile>().fireRight(player, launchSpeed, damage, MyAttribute);
        }
        
        else
        {
            proj = Instantiate(projectile, (this.transform.position + transform.up -transform.right), Quaternion.identity) as GameObject;
            proj.GetComponent<EyeCrawlerProjectile>().fireLeft(player, launchSpeed, damage, MyAttribute);
        }
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
                this.transform.position = leftBound.transform.position;
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
