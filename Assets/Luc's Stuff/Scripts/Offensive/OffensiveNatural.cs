using UnityEngine;
using System.Collections;

public class OffensiveNatural : Spell {

    GameObject player;
    public float Damage;
    public float Radius;
    public float duration;
    public float switcher;

	// Use this for initialization
	void Start ()
    {
        this.GetComponent<CircleCollider2D>().radius = Radius;
        player = GameObject.FindGameObjectWithTag("Player");

  
     StartCoroutine(Pulse());
        Destroy(this.gameObject, 3);

    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = player.transform.position;

	}


    IEnumerator Pulse()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Radius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.tag == "Enemy")
            {
                DamageManager.Instance.SendDamage(this.gameObject, hit.gameObject.GetComponent<Enemy>(), attribute.Natural, Damage, false);
            }
        }

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Pulse());
    }



    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(10.0f, status.MaxMana / 2);
        Damage = ManaCost * 2;
    }
}
