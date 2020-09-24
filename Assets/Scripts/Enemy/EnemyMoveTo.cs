using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveTo : MonoBehaviour
{
    public Transform Target { get; set; }

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _deltaSpeed;
    [SerializeField]
    private bool _moveWeird;
    [SerializeField]
    private float _baseDistance;
    [SerializeField]
    private bool _differentDistance;
    [SerializeField]
    private float _differenceDistanceDelta;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _speed += Random.Range(-_deltaSpeed, _deltaSpeed);
    }

    public void Update()
    {
        if (_moveWeird)
            _agent.speed = _speed + Random.Range(-_deltaSpeed, _deltaSpeed);
        else
            _agent.speed = _speed + _speed;

        _agent.destination = Target.position;
        transform.LookAt(new Vector3(Target.position.x, transform.position.y, Target.position.z));

        if (_differentDistance)
        {
            _agent.stoppingDistance = _baseDistance + Random.Range(0, _differenceDistanceDelta + 1);
        }        
    }
}
