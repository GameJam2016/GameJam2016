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

    private bool spawning;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (eyeCrawler.spawn)
        {
            StartCoroutine(spawnTimer(eyeCrawler));
        }

        if (golem.spawn)
        {
            StartCoroutine(spawnTimer(golem));
        }

        if (wisp.spawn)
        {
            StartCoroutine(spawnTimer(wisp));
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                location = (Random.insideUnitCircle * range)+ new Vector2(this.transform.position.x, this.transform.position.y);
                location.y = Mathf.Abs(location.y);
                spawnThing = Instantiate(toSpawn.enemyPrefab[(int)toSpawn.spawnAttribute], location, Quaternion.identity) as GameObject;
                spawnThing.GetComponent<Enemy>().leftBound = leftBound;
                spawnThing.GetComponent<Enemy>().rightBound = rightBound;
                spawnThing.GetComponent<Enemy>().spawner = this.gameObject;
                spawnThing.GetComponent<Enemy>().MyAttribute = toSpawn.spawnAttribute;
                
            }
        } while (toSpawn.spawnNumber != 0);
    }
}
