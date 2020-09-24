using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndShootProjectile : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 180.0f;
    [SerializeField]
    private float _attackSpeed = 1.0f;
    [SerializeField]
    private float _projectileSpeed = 10.0f;
    [SerializeField]
    private float _projectileDamage = 25.0f;
    [SerializeField]
    private float _attackSpeedDelta = 0.5f;

    private bool _attackPlayer;
    private bool _isDelay;
    private bool _targetPlayer;
    private Transform _playerTranform;
    private Transform _enemyTransform;

    private void Start()
    {
        _isDelay = false;
        _attackPlayer = true;
        _playerTranform = GameManager.Instance.Player.transform;
    }

    public void SetAttackMode(int mode)
    {
        if (mode == 0)
        {
            _targetPlayer = true;
        }
        else
        {
            _targetPlayer = false;
        }
    }

    private IEnumerator Shoot()
    {
        _isDelay = true;

        Vector3 shootPos = transform.position;
        shootPos.y += 0.65f;

        GameObject projectile = null;
        
        if (_attackPlayer)
        {
            projectile = Instantiate(Resources.Load("Enemy Projectile"), shootPos, Quaternion.identity) as GameObject;
            Projectile theProjectile = projectile.GetComponent<Projectile>();
            theProjectile.CanHitPlayer = true;
            theProjectile.CanHitEnemy = false;
            theProjectile.Damage = _projectileDamage;
            theProjectile.HitsLeft = 2;
        }
        else
        {
            projectile = Instantiate(Resources.Load("Projectile"), shootPos, Quaternion.identity) as GameObject;
            Projectile theProjectile = projectile.GetComponent<Projectile>();
            theProjectile.CanHitPlayer = false;
            theProjectile.CanHitEnemy = true;
            theProjectile.Damage = _projectileDamage;
            theProjectile.HitsLeft = 2;
        }

        projectile.GetComponent<Rigidbody>().velocity = transform.forward * (_projectileSpeed);

        yield return new WaitForSeconds(1 / (_attackSpeed + Random.Range(-_attackSpeedDelta, _attackSpeedDelta)));
        _isDelay = false;
    }

    private void Update()
    {
        if (_attackPlayer)
        {
            if (_targetPlayer)
            {
                // targets the player
                Vector3 temp = _playerTranform.position;

                temp.y = transform.position.y;
                transform.LookAt(temp);
                if (!_isDelay)
                {
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                // just spins around
                transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime, Space.Self);
                if (!_isDelay)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
        else
        {
            // targets the closest enemy
            Vector3 temp = EnemyManager.Instance.GetClosestEnemyPosition(transform);

            temp.y = transform.position.y;
            transform.LookAt(temp);
            if (!_isDelay)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    public void SetTarget(string id)
    {
        switch (id)
        {
            case "player":
                _attackPlayer = true;
                break;
            case "enemy":
                _attackPlayer = false;
                break;
            default:
                _attackPlayer = true;
                break;
        }
    }

    //public float RotateSpeed = 180.0f;

    //public float AttackSpeed = 1.0f;

    //public float AttackSpeedDelta = 0.0f;

    //public float ProjectileSpeed = 10.0f;

    //public float ProjectileDamage = 25.0f;

    //public Material OnColor;

    //public Material OffColor;

    //public bool TargetPlayer = false;

    //public bool IsOn = false;

    //public Transform Target;

    //private CharacterStats _characterStats;

    //private bool _isDelay;

    //public bool TargetClosestEnemy = false;

    //public List<GameObject> Lights;

    //private void Start()
    //{
    //    _characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
    //}

    //IEnumerator Shoot()
    //{
    //    _isDelay = true;
    //    ShootProjectile();

    //    if (TargetPlayer && !TargetClosestEnemy)
    //        yield return new WaitForSeconds(1 / ((AttackSpeed + Random.Range(-AttackSpeedDelta, AttackSpeedDelta)) / 2));
    //    else
    //        yield return new WaitForSeconds(1 / (AttackSpeed + Random.Range(-AttackSpeedDelta, AttackSpeedDelta)));

    //    _isDelay = false;
    //}

    //private void ShootProjectile()
    //{
    //    if (TargetClosestEnemy)
    //    {
    //        Vector3 shootPos = transform.position;
    //        shootPos.y += 0.65f;

    //        GameObject projectile = null;

    //        projectile = Instantiate(Resources.Load("Projectile"), shootPos, Quaternion.identity) as GameObject;
    //        projectile.GetComponent<Projectile>().Damage = ProjectileDamage;

    //        projectile.GetComponent<Projectile>().CanHitPlayer = false;
    //        projectile.GetComponent<Projectile>().HitsLeft = 2;
    //        projectile.GetComponent<Projectile>().CanHitEnemy = true;

    //        projectile.GetComponent<Projectile>().PlayerStats = _characterStats;
    //        projectile.GetComponent<Rigidbody>().velocity = transform.forward * (ProjectileSpeed);
    //    }
    //    if (!TargetClosestEnemy)
    //    {
    //        Vector3 shootPos = transform.position;
    //        shootPos.y += 0.65f;

    //        GameObject projectile = null;

    //        if (TargetPlayer)
    //        {
    //            projectile = Instantiate(Resources.Load("Enemy Projectile 2"), shootPos, Quaternion.identity) as GameObject;
    //            projectile.GetComponent<Projectile>().Damage = ProjectileDamage / 2;
    //        }
    //        else
    //        {
    //            projectile = Instantiate(Resources.Load("Enemy Projectile"), shootPos, Quaternion.identity) as GameObject;
    //            projectile.GetComponent<Projectile>().Damage = ProjectileDamage;
    //        }


    //        projectile.GetComponent<Projectile>().CanHitPlayer = true;
    //        projectile.GetComponent<Projectile>().HitsLeft = 1;
    //        projectile.GetComponent<Projectile>().CanHitEnemy = false;

    //        projectile.GetComponent<Projectile>().PlayerStats = _characterStats;
    //        projectile.GetComponent<Rigidbody>().velocity = transform.forward * (ProjectileSpeed);
    //    } 
    //}

    //public void TurnOn()
    //{
    //    foreach (GameObject part in Lights)
    //    {
    //        part.GetComponent<Renderer>().material = OnColor;
    //    }

    //    IsOn = true;
    //}

    //public void TurnOff()
    //{
    //    foreach (GameObject part in Lights)
    //    {
    //        part.GetComponent<Renderer>().material = OffColor;
    //    }

    //    IsOn = false;
    //}

    //private void Update()
    //{
    //    if (IsOn)
    //    {
    //        if (!_isDelay)
    //        {
    //            StartCoroutine(Shoot());
    //        }

    //        if (TargetClosestEnemy)
    //        {
    //            //Vector3 temp = GameManager.Instance.EnemyManager.GetClosestEnemy(transform).position;
    //            //temp.y = transform.position.y;
    //            //transform.LookAt(temp);
    //        }
    //        else if (TargetPlayer)
    //        {
    //            Vector3 temp = Target.position;
    //            temp.y = transform.position.y;

    //            transform.LookAt(temp);
    //        }
    //        else
    //        {
    //            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.Self);
    //        }            
    //    }
    //}
}
