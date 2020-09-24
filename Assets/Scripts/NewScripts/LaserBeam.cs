using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform FirePoint;
    public bool IsActive;

    public Vector3 Delta;

    private GameObject _laser;
    private LineRenderer _laserVisual;
    private BoxCollider _laserCollider;

    private void Start()
    {
        IsActive = false;
        _laser = Instantiate(Resources.Load("Laser Map/Map-Laser"), FirePoint.transform.position, Quaternion.identity) as GameObject;
        _laserVisual = _laser.GetComponent<LineRenderer>();
        _laserVisual.enabled = false;
        _laserCollider = _laser.GetComponent<BoxCollider>();
        _laserCollider.enabled = false;
        _laser.transform.forward = transform.forward;
        //_laser.SetActive(false);
    }

    public void Disable()
    {
        _laser.transform.forward = transform.forward;
        _laserVisual.enabled = false;
        _laserCollider.enabled = false;
        //_laser.SetActive(false);
    }

    public void ActivateFor(float t)
    {
        if (!IsActive)
        {
            StartCoroutine(PreLaser(t));
        }
    }

    private IEnumerator PreLaser(float t)
    {
        for (int i = 0; i < 5; i++)
        {
            //Debug.Log("loop" + i);
            _laserVisual.enabled = true;
            yield return new WaitForSeconds(0.15f);
            _laserVisual.enabled = false;
            yield return new WaitForSeconds(0.15f);
        }
   
        StartCoroutine(FireLaser(t));
    }

    private IEnumerator FireLaser(float t)
    {
        //_laser.SetActive(true);
        _laserVisual.enabled = true;
        _laserCollider.enabled = true;
        IsActive = true;
        _laser.transform.forward = transform.forward;// + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);// Random.Range(0, 0));
        yield return new WaitForSeconds(t);
        IsActive = false;
        _laserVisual.enabled = false;
        _laserCollider.enabled = false;
        //_laser.SetActive(false);
    }
}
