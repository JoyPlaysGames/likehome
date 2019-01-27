using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnInterval = 15f;
    public List<Transform> waypoints;
    public List<Transform> spawPoints;

    public List<Enemy> spawnedEnemies = new List<Enemy>();


    private void Awake()
    {
        The.enemyManager = this;
    }
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        int rand = Random.Range(0, enemies.Count);
        int spaw = Random.Range(0, spawPoints.Count);
		if (spawPoints != null)
		{
			GameObject e = Instantiate(enemies[rand], spawPoints[spaw].position, Quaternion.identity);
        
        Enemy e2 = e.GetComponent<Enemy>();
        e2.m = this;
        spawnedEnemies.Add(e2);
		}

		yield return new WaitForSeconds(spawnInterval + Random.Range(-5,5));
        StartCoroutine("SpawnEnemy");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DiededEnemy(Enemy e)
    {
        List<IngredientKind> items = e.dropsItem;
        Vector3 pos = e.transform.position;
        IngredientKind i = items[Random.Range(0, items.Count)];

        Instantiate(The.gameLogic.GetPrefabByKind(i), pos, Quaternion.identity);

        Destroy(e.transform.gameObject);
    }

    public void PlayerAtksEnemy(Vector3 pos)
    {
        for(int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] == null) { spawnedEnemies.RemoveAt(i); continue; }
            if(Vector3.Distance(spawnedEnemies[i].transform.position, pos) < 2.5f)
            {
                spawnedEnemies[i].GetAKickInAButt();
                break;
            }
        }
    }
}
