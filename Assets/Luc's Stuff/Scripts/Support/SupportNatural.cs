using UnityEngine;
using System.Collections;

public class SupportNatural : Spell {

    public float Duration;
    public float Radius;
    public GameObject player;
    public GameObject anim;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = player.transform.position;
        Duration = 0;
        this.GetComponent<CircleCollider2D>().radius = Radius;
        anim.transform.parent = gameObject.transform;
        
	}
	
	// Update is called once per frame
	void Update () {
        Duration += Time.deltaTime;
        if (Duration > 6)
            Destroy(this.gameObject);
    
	}

    public override void Randomize(PlayerStatus status)
    {
        ManaCost = Random.Range(10.0f, status.MaxMana / 2);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {        
            other.gameObject.GetComponent<Enemy>().crowdControl(this.gameObject, ManaCost, attribute.Natural);
        }
    }
}
