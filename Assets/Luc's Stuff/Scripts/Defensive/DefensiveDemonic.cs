using UnityEngine;
using System.Collections;

public class DefensiveDemonic : Spell
{
    public float HealAmount = 15.0f;
    public float InvisTime = 3.0f;

    private GameObject m_Player;

    // Use this for initialization
    void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Player.GetComponent<DamageableObject>().Health += HealAmount;
        StartCoroutine(COInvis());
    }

    IEnumerator COInvis()
    {
        //SpriteRenderer sprite = m_Player.GetComponentInParent<SpriteRenderer>();
        //if(!sprite)
        //{
        //    yield return null;
        //}
        //sprite.enabled = false;
        MeshRenderer render = m_Player.GetComponent<MeshRenderer>();
        render.enabled = false;
        yield return new WaitForSeconds(InvisTime);
        render.enabled = true;
      //  sprite.enabled = true;
    }

    public override void Randomize()
    {
        ManaCost = Random.Range(0.0f, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().MaxMana);
        HealAmount = ManaCost / 5.0f;
    }
}
