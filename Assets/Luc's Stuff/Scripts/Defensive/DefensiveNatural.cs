using UnityEngine;
using System.Collections;

public class DefensiveNatural : MonoBehaviour {

    public float HealAmount = 15.0f;
    public float ShieldTime = 2.0f;
    public float ShieldAmount = 20.0f;
    private GameObject m_Player;
    private float m_StartHealth;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player"); 
    
        m_Player.GetComponent<DamageableObject>().Health += HealAmount;
        m_StartHealth = m_Player.GetComponent<DamageableObject>().Health;
        m_Player.GetComponent<DamageableObject>().Health += ShieldAmount;
        Destroy(gameObject, ShieldTime);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnDestroy()
    {
        if(m_Player.GetComponent<DamageableObject>().Health > m_StartHealth)
        {
            m_Player.GetComponent<DamageableObject>().Health = m_StartHealth;
        }
    }
}
