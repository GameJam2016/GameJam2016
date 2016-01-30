using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour
{
    // The current instance of the Input manager
    public static DamageManager Instance = null;

    // Use this for initialization
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Sends Damage from one DamageableObject to another DamageableObject
    /// </summary>
    /// <param name="damageSender">DamageableObject that is sending the damage</param>
    /// <param name="damageTo">DamageableObject that is receiving the damage</param>
    /// <param name="damageType">The type of damage that is being taken</param>
    /// <param name="damageDone">The amount of damage that is being sent</param>
    /// <param name="bIsStun">Is this damage going to stun the receiver</param>
    public void SendDamage(GameObject damageSender, DamageableObject damageTo, attribute damageType, float damageDone, bool bIsStun)
    {
        // Checks whether we're strong, neutral or weak against the attack's attribute.

        /* If we're stronger. Eg: We're Holy (0) and the attack is Demonic (1), or we're Natural (2) and 
        the attack is Holy (0). */
        if (damageTo.MyAttribute - damageType == -1 || damageTo.MyAttribute - damageType == 2)
        {
            // Halve damage.
            damageDone = damageDone / 2;
        }

        // If we're weaker.
        else if (damageTo.MyAttribute - damageType == 1 || damageTo.MyAttribute - damageType == -2)
        {
            // Double damage.
            damageDone = damageDone * 2;
        }

        // If we're the same.
        else
        {

        }

        damageTo.Health -= damageDone;

        if (bIsStun)
        {
            damageTo.bIsStunned = true;
        }

        if (!damageTo.bIsDamaged)
        {
            StartCoroutine(damageTo.CODamageCooldown());
        }
    }
}
