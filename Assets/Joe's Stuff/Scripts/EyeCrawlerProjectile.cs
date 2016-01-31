using UnityEngine;
using System.Collections;

public class EyeCrawlerProjectile : MonoBehaviour
{
    private Rigidbody2D myRigid;
    [HideInInspector]
    public float projectileSpeed, damage;
    [HideInInspector]
    public attribute myAttribute;

    public GameObject attackCollider;
	// Use this for initialization
    void Awake ()
    {
        myRigid = this.GetComponent<Rigidbody2D>();
        attackCollider.GetComponent<CircleCollider2D>().enabled = false;
    }

	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // This function will be called to launch the projectile.
    public void fireLeft (GameObject player, float inputSpeed, float inputDamage, attribute inputAttribute)
    {
        projectileSpeed = inputSpeed;
        damage = inputDamage;
        myAttribute = inputAttribute;

        Vector2 launch = new Vector2((player.transform.position.x - this.transform.position.x), player.transform.position.y - this.transform.position.y).normalized * inputSpeed;
        myRigid.velocity = launch;
    }

    public void fireRight(GameObject player, float inputSpeed, float inputDamage, attribute inputAttribute)
    {
        projectileSpeed = inputSpeed;
        damage = inputDamage;
        myAttribute = inputAttribute;

        Vector2 launch = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y).normalized * inputSpeed;
        myRigid.velocity = launch;
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        attackCollider.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(die());
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DamageManager.Instance.SendDamage(this.gameObject, other.gameObject.GetComponent<DamageableObject>(), myAttribute, damage, false);
        }
    }

    IEnumerator die ()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
