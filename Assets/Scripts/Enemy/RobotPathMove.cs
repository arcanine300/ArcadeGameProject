using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotPathMove : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameObject MapPathPoints;

    public float TimeSpent = 10;

    public float Delta = 5;

    private bool delay = false;

    private List<Transform> points;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        MapPathPoints = GameObject.Find("EnemyPathPoints");

        points = new List<Transform>();
        foreach (Transform child in MapPathPoints.transform)
        {
            points.Add(child);
        }

        agent.destination = points[(int)Mathf.Floor(Random.Range(0, points.Count))].position;
    }

    IEnumerator Move()
    {
        delay = true;
        agent.destination = points[(int)Mathf.Floor(Random.Range(0, points.Count))].position;
        yield return new WaitForSeconds(TimeSpent + Random.Range(-Delta, Delta));
        delay = false;
    }

    private void Update()
    {
        if (!delay)
        {
            StartCoroutine(Move());
        }
    }
}
