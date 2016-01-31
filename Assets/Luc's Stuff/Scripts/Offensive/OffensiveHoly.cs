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
    public GameObject anim;

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
        float position = Random.Range(minRange, MaxRange);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(position * direction + player.transform.position.x, 20), new Vector2(0, -1));

    //    if (!hit)
    //        return;
        Debug.DrawLine(player.transform.position + new Vector3(position, 20 * direction, 0), hit.point, Color.red, 100);
        Debug.Log(hit.collider.tag);
        Instantiate(anim, hit.point + new Vector2(0, 3.5f), Quaternion.identity);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(hit.point - new Vector2(1, 0), new Vector2(1, 2), 0, Vector3.right);
        foreach(RaycastHit2D boxHit in hits)
        {
            if (boxHit.collider.gameObject.tag == "Enemy")
            {
                //      hit.collider.gameObject.GetComponent<Enemy>().Health -= Damage;
                DamageManager.Instance.SendDamage(this.gameObject, hit.collider.gameObject.GetComponent<Enemy>(), attribute.Holy, Damage, false);
            }
        }


     }



    public override void Randomize(PlayerStatus status)
    {

        ManaCost = Random.Range(10.0f, status.MaxMana / 2);
        Damage = ManaCost * 2;
    }
}
