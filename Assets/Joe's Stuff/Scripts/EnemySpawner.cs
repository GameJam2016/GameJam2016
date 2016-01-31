using UnityEngine;
using System.Collections;

[System.Serializable]
public class spawnChoice
{
    [SerializeField] public bool spawn;
    [SerializeField] public int spawnNumber;
    [SerializeField] public float spawnTime;
    [SerializeField] public attribute spawnAttribute;
    [SerializeField] public GameObject [] enemyPrefab = new GameObject [3];
};

public class EnemySpawner : MonoBehaviour
{
    // These structs contain a bool indicating whether the enemy will be spawned, how many should spawn at a time,
    // and how long an interval to wait between spawns.
    public spawnChoice eyeCrawler, golem, wisp;

    // These are the boundaries of the patrolling enemies Golems and Eye Crawlers. Place leftBound at the furthest left
    // point of their patrol, and take a wild guess what to do with the rightBound.
    public GameObject leftBound, rightBound;

    // The range indicates how far away they can be spawned, and how far the Wisps will meander while waitiing for a player.
    // timeDelay is the initial time to wait from when the spawner is onscreen before it starts spawning enemies.
    public float range, timeDelay;

    // onScreen indicates that the spawner is visible, and can start spawning. playerNear indicates a player is too near, and
    // no enemies will spawn.

    private bool onScreen, playerNear, spawning;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(this.transform.position).x < 1 && Camera.main.WorldToViewportPoint(this.transform.position).x > 0 && Camera.main.WorldToViewportPoint(this.transform.position).y < 1 && Camera.main.WorldToViewportPoint(this.transform.position).y > 0 && Camera.main.WorldToViewportPoint(this.transform.position).z > 0)
        {
            onScreen = true;
        }

        else
        {
            onScreen = false;
            spawning = false;
        }

        if ((player.transform.position - this.transform.position).magnitude > 5)
        {
            playerNear = false;
        }

        else
        {
            playerNear = true;
            spawning = false;
        }

        if (onScreen && !playerNear && !spawning)
        {
            spawning = true;
            StartCoroutine(startUp(timeDelay));
        }
    }

    IEnumerator startUp (float timeLimit)
    {
        float timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            yield return null;
        } while (timePassed < timeLimit);
        
        if (eyeCrawler.spawn)
        {
            StartCoroutine(spawnTimer(eyeCrawler));
        }

        if (wisp.spawn)
        {
            StartCoroutine(spawnTimer(wisp));
        }

        if (golem.spawn)
        {
            StartCoroutine(spawnTimer(golem));
        }
    }

    IEnumerator spawnTimer (spawnChoice toSpawn)
    {
        float timePassed = 0, timeLimit = toSpawn.spawnTime;
        GameObject spawnThing;
        Vector2 location;
        do
        {
            timePassed += Time.deltaTime;
            yield return null;

            if (timePassed > timeLimit)
            {
                timePassed = 0;
                toSpawn.spawnNumber--;
                location = Random.insideUnitCircle * range;
                location.y = Mathf.Abs(location.y);
                spawnThing = Instantiate(toSpawn.enemyPrefab[(int)toSpawn.spawnAttribute], location, Quaternion.identity) as GameObject;
                spawnThing.GetComponent<Enemy>().leftBound = leftBound;
                spawnThing.GetComponent<Enemy>().rightBound = rightBound;
                spawnThing.GetComponent<Enemy>().spawner = this.gameObject;
                spawnThing.GetComponent<Enemy>().MyAttribute = toSpawn.spawnAttribute;
            }
        } while (onScreen && !playerNear && toSpawn.spawnNumber != 0);
    }
}
