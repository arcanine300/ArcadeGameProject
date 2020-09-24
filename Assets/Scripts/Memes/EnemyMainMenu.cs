using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMainMenu : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 originalPos;
    public float movementDelay = 1.5f;
    public GameObject theArmy;

    // Start is called before the first frame update
    void Start()
    {
        theArmy.SetActive(false);
        _agent = GetComponent<NavMeshAgent>();
        originalPos = transform.position;
        _agent.destination = new Vector3(50f, transform.position.y, originalPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        //move right
        if (transform.position.x <= -50)
        {
            theArmy.SetActive(false);
            _agent.destination = new Vector3(50f, transform.position.y, originalPos.z);
            //transform.LookAt(new Vector3(-40f, transform.position.y, transform.position.z));
        }

        //move left
        if (transform.position.x >= 50f)
        {
            StartCoroutine(Delay());
            //_agent.destination = new Vector3(-40f, transform.position.y, transform.position.z);
            //transform.LookAt(new Vector3(40f, transform.position.y, transform.position.z));
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(movementDelay);
        theArmy.SetActive(true);
        _agent.destination = new Vector3(-50f, transform.position.y, originalPos.z);
    }
}
