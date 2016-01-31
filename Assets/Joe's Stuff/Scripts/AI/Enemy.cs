using UnityEngine;
using System.Collections;

public enum attribute
{
    Holy,
    Demonic,
    Natural
};


public class Enemy : DamageableObject
{
    public Rigidbody2D myRigid;
    public GameObject player;
    // Status effects.
   public bool damaged, stunned, crowdControlled, attacked;
    [HideInInspector] public GameObject leftBound, rightBound, spawner;
    // Initial speed, current speed, speed of attacks, the amount of time before you can be cc'd again, the scalar 
    // for cc forces, the scalar for slow cc.
    public float damage, startSpeed, moveSpeed, attackSpeed, crowdTime, pushPullScalar, slowScalar;

    
	// Use this for initialization
	void Start ()
    {
        crowdTime = 6.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    protected void CheckHealth()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void crowdControl (GameObject source, float manaCost, attribute attackType)
    {

        // This will be used in Holy or Demonic crowd control.
        Vector2 toSource = source.transform.position - this.transform.position;

        bool stronger, weaker;
        
        // If we're weaker.
        if (MyAttribute - attackType == -1 || MyAttribute - attackType == 2)
        {
            weaker = true;
            stronger = false;
        }

        // If we're stronger.
        else if (MyAttribute - attackType == 1 || MyAttribute - attackType == -2)
        {
            weaker = false;
            stronger = true;
        }

        // If we're the same type.
        else
        {
            weaker = false;
            stronger = false;
        }

        // If we're already crowd controlled, or we're outside it's range of influence, never mind.
        if (crowdControlled)
        {
                
        }
        
        // If it's Holy crowd control
        else if (attackType == attribute.Holy)
        {
            // Adjusts the mana cost based on wether we have the attribute advantage or not.
            // The mana cost is used as a scalar for the cc effects.

            if (stronger)
            {
                manaCost = manaCost / 2;
            }

            else if (weaker)
            {
                manaCost = manaCost * 2;
            }

            else
            {

            }

            // We get a vector away from the Holy CC, scaled by the pushPullScalar and the mana cost.
            toSource = -toSource.normalized * pushPullScalar * manaCost;

            // We then add a force equal to this vector to ourselves, shoving ourselves away.
            myRigid.AddForce(toSource);

            // We start our crowd control cooldown.
            StartCoroutine(crowdControlling(source));
        }

        // If it's Demonic crowd control
        else if (attackType == attribute.Demonic)
        {
            // Adjusts the mana cost based on wether we have the attribute advantage or not.
            // The mana cost is used as a scalar for the cc effects.

            if (stronger)
            {
                manaCost = manaCost / 2;
            }

            else if (weaker)
            {
                manaCost = manaCost * 2;
            }

            else
            {

            }

            // We get a vector towards the Demonic CC, scaled by the pushPullScalar and the mana cost.
            toSource = toSource.normalized * pushPullScalar * manaCost;

            // We then add a force equal to this vector to ourselves, pulling ourselves in.
            myRigid.AddForce(toSource);

            // We start our crowd control cooldown.
            StartCoroutine(crowdControlling(source));
        }

        // If it's Natural crowd control
        else if (attackType == attribute.Natural)
        {
            // Adjusts the mana cost based on wether we have the attribute advantage or not.
            // The mana cost is used as a scalar for the cc effects.

            if (stronger)
            {
                manaCost = manaCost / 2;
            }

            else if (weaker)
            {
                manaCost = manaCost * 2;
            }

            else
            {

            }
            
            // We start our crowd control cooldown.
            StartCoroutine(crowdControlling(source));

            // We start our slow timer
            StartCoroutine(slow(manaCost * slowScalar, source));
        }
    }

    public void die ()
    {
        Destroy(this.gameObject);
    }

    // How long we are crowd controlled.
    IEnumerator crowdControlling(GameObject source)
    {
        stunned = true;
        crowdControlled = true;
        do
        {
            yield return null;
        } while (source != null);
        crowdControlled = false;
        stunned = false;
    }

    // How long we are slowed.
    IEnumerator slow(float theSlow, GameObject source)
    {
        moveSpeed = moveSpeed / theSlow;
        stunned = true;
        do
        {
            yield return null;
        } while (source != null);
        moveSpeed = startSpeed;
        stunned = false;
    }

    // How long before we can attack again.
    public IEnumerator attackCooldown (float timeLimit)
    {
        float timePassed = 0;
        attacked = true;
        do
        {
            timePassed += Time.deltaTime;
            yield return null;
        } while (timePassed <= timeLimit);
        attacked = false;
    }
}
