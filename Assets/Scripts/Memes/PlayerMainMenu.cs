using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMainMenu : MonoBehaviour
{
    public float movementDelay = 1.5f;
    [SerializeField]
    private GameObject _shootPoint;
    private float _speed = 190.0f;
    private float _spread = 0.085f;
    private float _attackSpeed = 12.0f;
    private bool _isDelay;
    private bool _canShoot;
    private NavMeshAgent _agent;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        _isDelay = false;
        _agent = GetComponent<NavMeshAgent>();
        originalPos = transform.position;
        _agent.destination = new Vector3(50f, transform.position.y, originalPos.z);
        //transform.LookAt(new Vector3(-40f, transform.position.y, originalPos.z));
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canShoot)
        {
            if (!_isDelay)
            {
                StartCoroutine(Shoot());
            }
        } 

        //move right
        if (transform.position.x <= -50)
        {
            StartCoroutine(Delay());
            //_agent.destination = new Vector3(40f, transform.position.y, transform.position.z);
            //_canShoot = true;
        }

        //move left
        if (transform.position.x >= 50f)
        {
            _agent.destination = new Vector3(-50f, transform.position.y, originalPos.z);
            _canShoot = false;
        }

    }

    private IEnumerator Shoot()
    {
        _isDelay = true;
        Vector3 temp = _shootPoint.transform.position;
        temp.y = 1.3f;
        GameObject bullet = Instantiate(Resources.Load("Bullet"), temp, Quaternion.identity) as GameObject;
        Vector3 dir = _shootPoint.transform.forward;
        dir.x += Random.Range(-_spread, _spread);
        bullet.GetComponent<Rigidbody>().velocity = dir * _speed;
        yield return new WaitForSeconds(1 / _attackSpeed);
        _isDelay = false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(movementDelay);
        _agent.destination = new Vector3(50f, transform.position.y, originalPos.z);
        _canShoot = true;
    }
}
