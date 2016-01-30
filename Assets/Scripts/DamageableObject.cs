using UnityEngine;
using System.Collections;

public class DamageableObject : MonoBehaviour
{

    public float Health = 100;
    public attribute MyAttribute = attribute.Demonic;
    public bool bIsStunned = false;
    public bool bIsDamaged = false;
    public float DamageCooldown = 1.0f;


    /// <summary>
    /// Waits for a set amount of time then lets the DamageObject be allowed to get hurt again
    /// </summary>
    /// <returns></returns>
    public IEnumerator CODamageCooldown()
    {
        yield return new WaitForSeconds(DamageCooldown);
        bIsDamaged = false;
    }
}
