using UnityEngine;
using System.Collections;

public class SupportHoly : Spell {

    GameObject player;
    float duration;
    public GameObject anim;
	// Use this for initialization
	void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = player.transform.position;
        anim = (GameObject)Instantiate(anim, transform.position + new Vector3(0, 3, 0), transform.rotation);


    }
	
	// Update is called once per frame
	void Update () {
        if (duration > 0.1)
            Destroy(this.gameObject);
        duration += Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().crowdControl(this.gameObject,ManaCost , attribute.Holy);
            Debug.Log(other.tag);
        }
    
    }

    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(10.0f, status.MaxMana / 2);
    }
}
