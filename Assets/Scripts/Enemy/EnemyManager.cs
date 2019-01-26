using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnInterval = 15f;
    public List<Transform> waypoints;
    public List<Transform> spawPoints;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        int rand = Random.Range(0, enemies.Count);
        int spaw = Random.Range(0, spawPoints.Count);
        GameObject e = Instantiate(enemies[rand], spawPoints[spaw].position, Quaternion.identity);
        e.GetComponent<Enemy>().m = this;

        yield return new WaitForSeconds(spawnInterval + Random.Range(-5,5));
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
