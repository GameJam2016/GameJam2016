using UnityEngine;
using System.Collections;

public class OffensiveNatural : MonoBehaviour {

    GameObject player;
    public float Damage;
    public float Radius;

	// Use this for initialization
	void Start () {
        this.GetComponent<CircleCollider2D>().radius = Radius;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = player.transform.position;
	}
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            DamageManager.Instance.SendDamage(this.gameObject, other.gameObject.GetComponent<Enemy>(), attribute.Holy, Damage, false);
        }
    }
}
