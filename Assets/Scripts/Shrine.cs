using UnityEngine;
using System.Collections;

public class Shrine : MonoBehaviour
{
    private bool FirstTime = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(FirstTime)
            {
                other.gameObject.GetComponent<PlayerStatus>().AddSpell(3);
            }
        }
    }
}
