using UnityEngine;
using System.Collections;

public class OffensiveDenomic : MonoBehaviour {

    GameObject player;
    public int direction;
    public float Damage;
    public float maxRange;
   public  RaycastHit[] hits;

	// Use this for initialization
	void Start () {

     //   player = GameObject.FindGameObjectWithTag("Player");
        player = this.gameObject;
        if (player.transform.localScale.x < 0)
            direction = -1;
        else if (player.transform.localScale.x > 0)
            direction = 1;

        Debug.DrawLine(player.transform.position, player.transform.position + new Vector3(player.transform.position.x + (maxRange * direction), player.transform.position.y, player.transform.position.z), Color.white, 100);

        if (Physics.Raycast(player.transform.position, new Vector3(direction, 0, 0), maxRange))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag == "Enemy")
                {
                    DamageManager.Instance.SendDamage(this.gameObject, hits[i].collider.gameObject.GetComponent<Enemy>(), attribute.Holy, Damage, false);
                }
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
