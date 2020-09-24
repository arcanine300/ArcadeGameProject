using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryController : MonoBehaviour, IMapGimmick
{
    [SerializeField]
    private float _timeBetweenLaunches = 5.0f;
    [SerializeField]
    private float _areaRadius = 10.0f;
    [SerializeField]
    private int _blastAreaCount = 4;
    [SerializeField]
    private int _blastAreaCountDelta = 2;
    [SerializeField]
    private float _blastAreaSpawnDelay = 0.1f;
    [SerializeField]
    private float _blastWarningLifeTime = 3.0f;
    [SerializeField]
    private int _amountOfGrenadesPerVolley = 3;
    [SerializeField]
    private float _grenadeVolleyFireRate = 0.5f;
    [SerializeField]
    private int _grenadeSpawnVelocity = 100;
    [SerializeField]
    private float _grenadeDamage = 25f;


    private bool _isActive = false;
    private bool _isBombing = false;

    [SerializeField]
    private GameObject _blastWarning;
    [SerializeField]
    private GameObject _shell;

    private bool _canHitPlayer = true;

    private void Update()
    {
        if (_isActive)
        {
            if (!_isBombing)
            {
                StartCoroutine(Process());
            }
        }
    }

    private IEnumerator Process()
    {
        _isBombing = true;
        Vector3[] points = GetAreas();

        for (int i = 0; i < points.Length; i++)
        {
            StartCoroutine(Launch(points[i]));
            yield return new WaitForSeconds(_blastAreaSpawnDelay);
        }

        yield return new WaitForSeconds(_timeBetweenLaunches);
        _isBombing = false;
    }

    private IEnumerator Launch(Vector3 point)
    {
        GameObject warning = Instantiate(_blastWarning, new Vector3(point.x, 1.0f, point.y), Quaternion.identity);

        yield return new WaitForSeconds(_blastWarningLifeTime);

        Destroy(warning);

        for (int i = 0; i < _amountOfGrenadesPerVolley; i++)
        {
            GameObject shell = Instantiate(_shell, new Vector3(point.x, 100.0f, point.y), Quaternion.identity);
            shell.GetComponent<GrenadeImpact>().Damage = _grenadeDamage;
            shell.GetComponent<Rigidbody>().velocity = Vector3.down * _grenadeSpawnVelocity;

            if (_canHitPlayer)
            {
                shell.GetComponent<GrenadeImpact>().CanHitPlayer = true;
            }
            else
            {
                shell.GetComponent<GrenadeImpact>().CanHitPlayer = false;
            }
            yield return new WaitForSeconds(_grenadeVolleyFireRate);
        }
    }

    private Vector3[] GetAreas()
    {
        int amount = _blastAreaCount + Random.Range(-_blastAreaCountDelta, _blastAreaCountDelta + 1);
        Vector3[] points = new Vector3[amount];

        for (int i = 0; i < amount; i++)
        {
            points[i] = new Vector3(Random.Range(-_areaRadius, _areaRadius + 1), Random.Range(-_areaRadius, _areaRadius + 1), Random.Range(-_areaRadius, _areaRadius + 1));
        }

        return points;
    }

    public void Begin()
    {
        _isActive = true;
    }

    public void Stop()
    {
        _isActive = false;
    }

    public void UseItem()
    {
        Debug.Log("Artillery Item");
    }

    void OnDrawGizmos()
    {
        // Draw a sphere around the transform's position in the editor view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _areaRadius);
    }
}
