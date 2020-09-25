using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileAtPlayer : MonoBehaviour
{
    [SerializeField]
    private float _damage = 10.0f;

    [SerializeField]
    private float _attackSpeed = 10.0f;

    [SerializeField]
    private float _projectileSpeed = 22.5f;

    [SerializeField]
    private float _attackSpeedDelta = 5.0f;

    private bool _isDelay;

    private void Start()
    {
        _isDelay = false;
    }

    IEnumerator Shoot()
    {
        _isDelay = true;
        ShootProjectile();
        yield return new WaitForSeconds(1 / (_attackSpeed + UnityEngine.Random.Range(-_attackSpeedDelta, _attackSpeedDelta)));
        _isDelay = false;
    }

    private void ShootProjectile()
    {
        Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        GameObject projectile = Instantiate(Resources.Load("Enemy Projectile"), bulletPos, Quaternion.identity) as GameObject;
        Projectile theProjectile = projectile.GetComponent<Projectile>();
        theProjectile.CanHitPlayer = true;
        theProjectile.HitsLeft = 1;
        theProjectile.CanHitEnemy = false;
        theProjectile.Damage = _damage;
        // change to vector3.forward when the model is fixed cuz cyclinder is facing up so its forward is down
        projectile.GetComponent<Rigidbody>().velocity = (GetComponent<EnemyRangeAI>().Target.transform.position - transform.position).normalized * (_projectileSpeed);
    }

    private void Update()
    {
        if (!_isDelay)
        {
            StartCoroutine(Shoot());
        }
    }
}
