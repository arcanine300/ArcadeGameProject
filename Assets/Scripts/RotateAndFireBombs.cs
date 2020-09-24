using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndFireBombs : MonoBehaviour
{
    public float RotateSpeed = 180.0f;

    public float AttackSpeed = 1.0f;

    public float AttackSpeedDelta = 0.0f;

    public float ProjectileSpeed = 10.0f;

    public float ProjectileDamage = 25.0f;

    public float Damage = 50f;

    public CharacterStats CharacterStats;

    private bool _isDelay;

    IEnumerator Shoot()
    {
        _isDelay = true;
        ShootBombs();
        yield return new WaitForSeconds(1 / (AttackSpeed + Random.Range(-AttackSpeedDelta, AttackSpeedDelta)));

        _isDelay = false;
    }

    private bool rotateDelay = false;

    public float RotateDelay = 1;

    IEnumerator Rotate()
    {
        rotateDelay = true;
        transform.Rotate(Vector3.up * RotateSpeed, Space.Self);
        yield return new WaitForSeconds(RotateDelay);
        rotateDelay = false;
    }

    public List<GameObject> Turrets;

    private void ShootBombs()
    {
        foreach (var turret in Turrets)
        {
            GameObject grenade = Instantiate(Resources.Load("Grenade"), turret.transform.position + new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            GrenadeImpact impact = grenade.GetComponent<GrenadeImpact>();
            impact.Damage = Damage;
            impact.CanHitEnemy = false;
            impact.CanHitPlayer = true;
            grenade.GetComponent<Rigidbody>().velocity = turret.transform.up * (ProjectileSpeed + Random.Range(-ProjectileSpeed / 2, ProjectileSpeed / 2));
        }
    }

    private void Update()
    {
        if (!_isDelay)
        {
            StartCoroutine(Shoot());
        }

        if (!rotateDelay)
        {
            StartCoroutine(Rotate());
        }        
    }
}
