using UnityEngine;
using System.Collections;

public class DefensiveHoly : Spell
{

    public float HealAmount = 15.0f;

    public float HealRadiationAmount = 10.0f;
    public float HealRadiationDistance = 5.0f;
    public float RadiationLifeTime = 5.0f;

    private GameObject m_RadiationObject;
    private GameObject m_Player;

    // Use this for initialization
    void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Player.GetComponent<DamageableObject>().Health += HealAmount;
        m_RadiationObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        m_RadiationObject.transform.position = m_Player.gameObject.transform.position;
        Destroy(m_RadiationObject, RadiationLifeTime);
        Destroy(gameObject, RadiationLifeTime);
        StartCoroutine(COHealPulse());
    }
	
	// Update is called once per frame
	void Update ()
    {
        
            
	}

    IEnumerator COHealPulse()
    {
        yield return new WaitForSeconds(1.0f);

        float distance = Vector3.Distance(m_Player.transform.position, m_RadiationObject.transform.position);
        if (distance <= HealRadiationDistance)
        {
            if (distance != 0)
            {
                m_Player.GetComponent<DamageableObject>().Health += HealAmount * (1 - (distance / HealRadiationDistance));
            }
            else
            {
                m_Player.GetComponent<DamageableObject>().Health += HealAmount;
            }
        }
        StartCoroutine(COHealPulse());
    }


    public override void Randomize()
    {
        ManaCost = Random.Range(0.0f, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().MaxMana);
        HealAmount = ManaCost / 5.0f;

    }
}
