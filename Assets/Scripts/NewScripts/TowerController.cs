using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour, IMapGimmick
{
    [SerializeField]
    private List<GameObject> _towers;
    [SerializeField]
    private float _useItemTime = 10.0f;

    private void Start()
    {
        SetTowerAttackModes();
    }

    private void SetTowerAttackModes()
    {
        foreach (GameObject tower in _towers)
        {
            RotateAndShootProjectile r = tower.GetComponent<RotateAndShootProjectile>();
            r.SetAttackMode(0);
        }
    }

    public void Begin()
    {
        foreach (GameObject tower in _towers)
        {
            tower.GetComponent<RotateAndShootProjectile>().enabled = true;
        }
    }

    public void Stop()
    {
        foreach (GameObject tower in _towers)
        {
            tower.GetComponent<RotateAndShootProjectile>().enabled = false;
        }
    }

    private IEnumerator ItemUsed()
    {
        foreach (GameObject tower in _towers)
        {
            tower.GetComponent<RotateAndShootProjectile>().SetTarget("enemy");
        }
        yield return new WaitForSeconds(_useItemTime);
        foreach (GameObject tower in _towers)
        {
            tower.GetComponent<RotateAndShootProjectile>().SetTarget("player");
        }
    }
    
    public void UseItem()
    {
        StartCoroutine(ItemUsed());
    }
}
