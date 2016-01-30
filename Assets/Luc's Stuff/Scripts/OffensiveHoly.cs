using UnityEngine;
using System.Collections;

public class OffensiveHoly : MonoBehaviour {

    public int minRange;
    public int MaxRange;
    public float duration;
    public float Damage;
    public GameObject player;
    public GameObject enemy;
    public float direction;
    int beamNumber;

	// Use this for initialization
	void Start () {
        beamNumber = 0;
        duration = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.localScale.x < 0)
            direction = -1;
        else if (player.transform.localScale.x > 0)
            direction = 1;
        Beam();
        beamNumber++;
	
	}
	
	// Update is called once per frame
	void Update () {
        duration += Time.deltaTime;
        if(duration > 1 && beamNumber == 1)
        {
            Beam();
            beamNumber++;
        }
        if (duration > 2 && beamNumber == 2)
        {
            Beam();
            Destroy(this.gameObject);
        }
	}

    void Beam()
    {
       float position = Random.Range(minRange,MaxRange);
        
        RaycastHit hit;
       if (Physics.Raycast(player.transform.position + new Vector3(position * direction, 20, 0), new Vector3(0, -1, 0),  out hit))
       {
           Debug.DrawLine(player.transform.position + new Vector3(position, 20 * direction, 0), hit.point, Color.white, 100);
          if(hit.collider.gameObject.tag == "Enemy")
          {
              DamageManager.Instance.SendDamage(this.gameObject, hit.collider.gameObject.GetComponent<Enemy>(), attribute.Holy, Damage, false);
          }
           
       }

        
    }
}
