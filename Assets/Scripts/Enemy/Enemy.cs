using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyKind
{
    None = 0,
    Spider = 1,
    Snake = 2,
}

public class Enemy : MonoBehaviour
{
    public EnemyKind enemyKind;
    public List<IngredientKind> dropsItem;
    public int hp;
    public int atk;
    public float speed = 10f;

    public List<Vector3> tempPoints;

    Vector3 currTarget = Vector3.zero;

    public EnemyManager m;

    // Start is called before the first frame update
    void Start()
    {
        currTarget = FindWaypoint(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currTarget, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, currTarget) < 0.5)
        {
            currTarget = FindWaypoint();
        }
        Vector3 targetDir = currTarget - transform.position;
        float step = 2f * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    Vector3 FindWaypoint()
    {
        Vector3 p1 = Vector3.zero;
        float p1_dist = 9999f;
        Vector3 p2 = Vector3.zero;
        Vector3 p3 = Vector3.zero;
        List<Transform> all = m.waypoints;
        foreach(Transform t in all)
        {
            Vector3 pos = t.position;
            if(Vector3.Distance(transform.position, pos) < p1_dist && pos != currTarget)
            {
                p3 = p2; p2 = p1; p1 = pos; p1_dist = Vector3.Distance(transform.position, pos);
                if(p3 == Vector3.zero) p3 = p2;
            }
        }
        List<Vector3> points = new List<Vector3>(); points.Add(p1); points.Add(p2); points.Add(p3);
        tempPoints = points;
        return points[Random.Range(0, points.Count)];
    }

}
