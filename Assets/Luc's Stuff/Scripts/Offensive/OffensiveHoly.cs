using UnityEngine;
using System.Collections;

public class OffensiveHoly : Spell
{

    public int minRange;
    public int MaxRange;
    public float duration;
    public float Damage;
    public GameObject player;
    public GameObject enemy;
    public float direction;
    int beamNumber;
    public GameObject currentBeam;
    public GameObject BeamAnimation;

	// Use this for initialization
	void Start () {
        beamNumber = 0;
        duration = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = player.transform.position;
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
            beamNumber++;

        }
        if (duration > 3)
            Destroy(this.gameObject);
	}

    void Beam()
    {
        if (currentBeam != null)
            Destroy(currentBeam);
        float position = Random.Range(minRange, MaxRange);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2((position * direction )+ this.transform.position.x, this.transform.position.y + 3), new Vector2(0, -1));
    //    if (!hit)
    //        return;
        Debug.DrawLine(this.transform.position + new Vector3(position * direction, this.transform.position.y + 3, 0), hit.point, Color.red, 100);
      //  Debug.Log(hit.collider.tag);
         currentBeam = (GameObject)Instantiate(BeamAnimation, hit.point + new Vector2(0,2),Quaternion.identity);
         currentBeam.transform.parent = this.transform;

        if (hit.collider.gameObject.tag == "Enemy")
        {
           hit.collider.gameObject.GetComponent<Enemy>().Health -= Damage;
            DamageManager.Instance.SendDamage(this.gameObject, hit.collider.gameObject.GetComponent<Enemy>(), attribute.Holy, Damage, false);
        }

     }



    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(0.0f, status.MaxMana);
        Damage = ManaCost * 5;
    }
}
