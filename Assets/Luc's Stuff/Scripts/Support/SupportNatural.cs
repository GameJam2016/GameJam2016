using UnityEngine;
using System.Collections;

public class SupportNatural : Spell {

    public float Duration;
    public float Radius;
    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = player.transform.position;
        Duration = 0;
        this.GetComponent<CircleCollider2D>().radius = Radius;
	}
	
	// Update is called once per frame
	void Update () {
        Duration += Time.deltaTime;
        if (Duration > 6)
            Destroy(this.gameObject);
    
	}

    public override void Randomize(PlayerStatus status)
    {
        ManaCost = Random.Range(0.0f, status.MaxMana);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Billy");
            other.gameObject.GetComponent<Enemy>().crowdControl(this.gameObject, ManaCost, attribute.Natural);
        }
    }
}
