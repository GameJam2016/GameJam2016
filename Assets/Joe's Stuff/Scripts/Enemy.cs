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
    private Rigidbody2D myRigid;

    [HideInInspector] public attribute myAttribute;
    // Status effects.
    [HideInInspector] public bool damaged, stunned, crowdControlled;

    // Initial speed, current speed, speed of attacks, the amount of time before you can be cc'd again, the scalar 
    // for cc forces, the scalar for slow cc.
    public float startSpeed, moveSpeed, attackSpeed, crowdTime, pushPullScalar, slowScalar;

	// Use this for initialization
	void Start ()
    {
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void crowdControl (GameObject source, int manaCost, attribute attackType)
    {
        // This will be used in Holy or Demonic crowd control.
        Vector2 toSource = source.transform.position - this.transform.position;

        bool stronger, weaker;
        
        // If we're weaker.
        if (myAttribute - attackType == -1 || myAttribute - attackType == 2)
        {
            weaker = true;
            stronger = false;
        }

        // If we're stronger.
        else if (myAttribute - attackType == 1 || myAttribute - attackType == -2)
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
        if (crowdControlled || toSource.magnitude > manaCost)
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
            StartCoroutine(crowdControlling(crowdTime));
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

            // We get a vector away from the Holy CC, scaled by the pushPullScalar and the mana cost.
            toSource = toSource.normalized * pushPullScalar * manaCost;

            // We then add a force equal to this vector to ourselves, pulling ourselves in.
            myRigid.AddForce(toSource);

            // We start our crowd control cooldown.
            StartCoroutine(crowdControlling(crowdTime));
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
            StartCoroutine(crowdControlling(crowdTime));

            // We start our slow timer
            StartCoroutine(slow(crowdTime, manaCost * slowScalar));
        }
    }

    public void die ()
    {
        Destroy(this.gameObject);
    }

    IEnumerator crowdControlling(float timeLimit)
    {
        float timePassed = 0;
        crowdControlled = true;
        do
        {
            timePassed += Time.deltaTime;
            yield return null;
        } while (timePassed <= timeLimit);
        crowdControlled = false;
    }

    IEnumerator slow(float timeLimit, float theSlow)
    {
        float timePassed = 0;
        moveSpeed -= theSlow;
        do
        {
            timePassed += Time.deltaTime;
            yield return null;
        } while (timePassed <= timeLimit);
        moveSpeed = startSpeed;
    }
}
