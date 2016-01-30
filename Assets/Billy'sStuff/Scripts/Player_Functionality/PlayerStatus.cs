using UnityEngine;
using System.Collections;

public class PlayerStatus : DamageableObject
{

    public attribute myAttribute;
    public float MaxHealth, currentHealth;
    public bool damaged, stunned;

    

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    public void takeDamage(float damage, attribute attackType, bool stun)
    {
        //if we're stronger
        if(myAttribute - attackType == -1 || myAttribute - attackType == 2)
        {
            //halve damage
            currentHealth -= damage / 2;
        }

        //if we're weaker 
        else if(myAttribute - attackType == 1 || myAttribute - attackType == -2)
        {
            //double damage
            currentHealth -= damage * 2;
        }
        //if we're the same attribute
        else
        {

        }

        if(stun)
        {
            stunned = true;
        }
        damaged = true;
    }

    public void die()
    {
        Destroy(this.gameObject);
    }
}
