using UnityEngine;
using System.Collections;

public class SupportHoly : Spell {

    GameObject player;
    float duration;
	// Use this for initialization
	void Start () {
        duration += Time.deltaTime;
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = player.transform.position;

        
	}
	
	// Update is called once per frame
	void Update () {
        if (duration > 6)
            Destroy(this.gameObject);
	
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

        ManaCost = Random.Range(0.0f, status.MaxMana);
    }
}
