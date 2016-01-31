using UnityEngine;
using System.Collections;

public class OffensiveDemonic : Spell
{

    GameObject player;
    public int direction;
    public float Damage;
    public float maxRange;
   public  RaycastHit2D[] hits;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        //  player = this.gameObject;
        if (player.transform.localScale.x < 0)
            direction = -1;
        else if (player.transform.localScale.x > 0)
            direction = 1;

        Debug.DrawLine(player.transform.position, player.transform.position + (direction * (new Vector3(player.transform.position.x + maxRange , 0, 0))), Color.white, 100);
        hits = Physics2D.RaycastAll(player.transform.position, new Vector3(direction, 0, 0));
    
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag == "Enemy")
            {
                Debug.Log(hits[i].collider.gameObject.tag);
             //   hits[i].collider.gameObject.GetComponent<Enemy>().Health -= Damage;
                DamageManager.Instance.SendDamage(this.gameObject, hits[i].collider.gameObject.GetComponent<Enemy>(), attribute.Demonic, Damage, false);
            }
        }

        
	}


    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(0.0f, status.MaxMana);
        Damage = ManaCost * 5;
    }
}
