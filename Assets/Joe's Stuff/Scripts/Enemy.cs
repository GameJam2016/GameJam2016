using UnityEngine;
using System.Collections;

public enum attribute
{
    Holy,
    Demonic,
    Natural
};


public class Enemy : MonoBehaviour
{
    [HideInInspector] public float health;
    [HideInInspector] public attribute myAttribute;
    [HideInInspector] public bool damaged, stunned;

    public float startHealth;
    public float startSpeed;
    public float attackSpeed;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    /* This will be called by scripts that would damage enemies, like Player or Spells. It will pass what
    damage to take, what attribute the attack is, and whether the enemy is stunned. Based on this information,
    the enemy will take the appropriate damage*/
    public void takeDamage (float damage, attribute attackType, bool stun)
    {
        // Checks whether we're strong, neutral or weak against the attack's attribute.

        /* If we're stronger. Eg: We're Holy (0) and the attack is Demonic (1), or we're Natural (2) and 
        the attack is Holy (0). */
        if (myAttribute - attackType == -1 || myAttribute - attackType == 2)
        {
            // Halve damage.
            damage = damage / 2;
        }

        // If we're weaker.
        else if (myAttribute - attackType == 1 || myAttribute - attackType == -2)
        {
            // Double damage.
            damage = damage * 2;
        }

        // If we're the same.
        else
        {

        }

        health -= damage;

        if (stun)
        {
            stunned = true;
        }

        damaged = true;
    }

    public void die ()
    {
        Destroy(this.gameObject);
    }
}
