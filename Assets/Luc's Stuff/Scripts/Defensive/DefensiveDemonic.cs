using UnityEngine;
using System.Collections;

public class DefensiveDemonic : Spell
{
    public float HealAmount = 15.0f;
    public float InvisTime = 3.0f;

    private GameObject m_Player;
    public GameObject m_Anim;

    // Use this for initialization
    void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Player.GetComponent<DamageableObject>().Health += HealAmount;
        StartCoroutine(COInvis());
        
        m_Anim = (GameObject)Instantiate(m_Anim, m_Player.transform.position, transform.rotation);
        m_Anim.transform.parent = this.transform;
    }

    IEnumerator COInvis()
    {
        SpriteRenderer sprite = m_Player.GetComponentInChildren<SpriteRenderer>();
        if (!sprite)
        {
            yield return null;
        }
        sprite.enabled = false;
        m_Player.GetComponent<PlayerStatus>().bIsInvisible = true;
        //  MeshRenderer render = m_Player.GetComponent<MeshRenderer>();
        //   render.enabled = false;
        yield return new WaitForSeconds(InvisTime);
        //render.enabled = true;
        sprite.enabled = true;
        m_Player.GetComponent<PlayerStatus>().bIsInvisible = false;
        Destroy(gameObject);
    }


    void Update()
    {
        if(m_Anim)
        {
            m_Anim.transform.position = m_Player.transform.position;
        }
    }

    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(10.0f, status.MaxMana / 2);
        HealAmount = ManaCost * 2.0f;
    }
}
