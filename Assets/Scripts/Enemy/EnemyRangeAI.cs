using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyRangeAI : MonoBehaviour
{
    public bool Active = true;
    public Transform Target;
    public float MinRange = 10.0f;
    public float MaxRange = 15.0f;
    public float SingleAreaTime = 5.0f;
    public float ClosenessThreshold = 1.0f;
    public float Speed = 5.0f;

    private bool iterationActive = false;
    private NavMeshAgent agent;
    private float restingAngle;
    private bool initialStoppingDistanceDone = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;

        Vector3 targetDir = Target.position - transform.position;
        float angle = Vector3.Angle(targetDir, Vector3.right);

        //restingAngle = Random.Range(0, 360);

        restingAngle = angle + Random.Range(-10, 10) + 180;

        StartCoroutine(GoToNewArea());
    }

    private void Update()
    {
        if (Active)
        {
            if (!iterationActive)
            {
                StartCoroutine(GoToNewArea());
            }
            if (Target.GetComponent<IsMoving>().Result)
            {
                StartCoroutine(DelayMove(.2f));
            }

            transform.LookAt(Target);
        }        
    }

    private IEnumerator DelayMove(float s)
    {
        yield return new WaitForSeconds(s);
        StartCoroutine(AreaPlus());
    }

    private IEnumerator GoToNewArea()
    {
        iterationActive = true;
        agent.destination = GetRandomPosition();
        yield return new WaitForSeconds(SingleAreaTime + Random.Range(0, 1));
        if (!initialStoppingDistanceDone)
        {
            initialStoppingDistanceDone = true;
            agent.stoppingDistance = 0;
        }
        iterationActive = false;
    }

    private IEnumerator AreaPlus()
    {
        agent.destination = GetRandomPosition();
        yield return new WaitForSeconds(SingleAreaTime);
        if (!initialStoppingDistanceDone)
        {
            initialStoppingDistanceDone = true;
            agent.stoppingDistance = 0;
        }
    }

    private Vector3 GetRandomPosition()
    {
        float delta = 25.0f;
        float a = restingAngle + Random.Range(-delta, delta);

        //a = a > 360 ? a + 360 : a < 0 ? a - 360 : a;

        float x = Mathf.Cos(Mathf.Deg2Rad * a) * Random.Range(MinRange, MaxRange);
        float z = Mathf.Sin(Mathf.Deg2Rad * a) * Random.Range(MinRange, MaxRange);
        //Debug.Log($"New pos: x {x + Target.position.x} y {Target.position.y} z {z + Target.position.z}");
        return new Vector3(x + Target.position.x, Target.position.y, z + Target.position.z);
    }

    private bool IsTooCloseToTarget()
    {
        return (Vector3.Distance(transform.position, Target.position) - ClosenessThreshold) > MinRange;
    }
}
