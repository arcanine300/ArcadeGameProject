using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultRifleShoot : MonoBehaviour, IWeapon
{    
    [SerializeField]
    private GameObject _shootPoint;
    [SerializeField]
    private float _damage = 10.0f;
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private float _spread = 0.0f;
    [SerializeField]
    private float _attackSpeed = 10.0f;

    private CharacterStats _stats;

    private void Awake()
    {
        GetComponent<Weapon>().WeaponAction = this;        
    }

    private void Start()
    {
        _stats = GameManager.Instance.Player.GetComponent<CharacterStats>();
    }

    public void Shoot()
    {
        Vector3 temp = _shootPoint.transform.position;
        temp.y = 1.3f;
        GameObject bullet = Instantiate(Resources.Load("Bullet"), temp, Quaternion.identity) as GameObject;
        Bullet b = bullet.GetComponent<Bullet>();
        b.applyBurnEffect = false;

        if (_stats == null)
        {
            b.Damage = _damage;
        }
        else
        {
            b.Damage = _damage * _stats.DamageMultiplier;
        }        
        Vector3 dir = _shootPoint.transform.forward;
        dir.x += Random.Range(-_spread, _spread);

        bullet.GetComponent<Rigidbody>().velocity = dir * _speed;
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }
}