using UnityEngine;
using System.Collections;

public class SupportDemonic :  Spell {

    public float range;
    public GameObject player;
    float duration;
    bool BHole;
    public float speed;
    float direction;
    float BholeRadius;
	// Use this for initialization
	void Start () {
        
        BHole = false;
        duration = 0;
       
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.localScale.x > 0)
            direction = 1;
        else if (player.transform.localScale.x < 0)
            direction = -1;

        this.transform.position = player.transform.position + new Vector3(direction,0,0);
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(BHole)
        duration += Time.deltaTime;
        if (duration > 6)
            Destroy(this.gameObject);
	}


    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(0.0f, status.MaxMana);
    }

    void shoot()
    {
        if (!BHole)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(direction, 0, 0) * speed;
            if ((this.transform.position - player.transform.position).magnitude > range)
            {
                BlackHole();
                BHole = true;
            }
        }
    }

    void BlackHole()
    {
        this.GetComponent<CircleCollider2D>().radius = BholeRadius;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            BlackHole();
            BHole = true;
        }
        if(other.gameObject.tag == "Enemy" && BHole == true)
        {
            other.gameObject.GetComponent<Enemy>().crowdControl(this.gameObject, ManaCost, attribute.Demonic);
        }

    }
}
