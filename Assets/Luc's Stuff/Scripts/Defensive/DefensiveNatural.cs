using UnityEngine;
using System.Collections;

public class DefensiveNatural : Spell
{

    public float HealAmount = 15.0f;
    public float ShieldTime = 2.0f;
    public float ShieldAmount = 20.0f;
    private GameObject m_Player;
    private float m_StartHealth;
    [SerializeField]
    private GameObject m_anim;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");

        m_Player.GetComponent<DamageableObject>().Health += HealAmount;
        m_StartHealth = m_Player.GetComponent<DamageableObject>().Health;
        m_Player.GetComponent<DamageableObject>().Health += ShieldAmount;
        m_anim = (GameObject)Instantiate(m_anim, transform.position, transform.rotation);
        m_anim.transform.parent = gameObject.transform;
        Destroy(gameObject, ShieldTime);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = m_Player.transform.position;
	}

    void OnDestroy()
    {
        
        if(m_Player.GetComponent<DamageableObject>().Health > m_StartHealth)
        {
            m_Player.GetComponent<DamageableObject>().Health = m_StartHealth;
            
        }
    }



    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(0.0f, status.MaxMana);
        HealAmount = ManaCost / 5.0f;
    }
}
