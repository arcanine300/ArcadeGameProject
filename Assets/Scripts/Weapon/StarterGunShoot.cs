using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterGunShoot : MonoBehaviour, IWeapon
{
    [SerializeField]
    private GameObject _shootPoint;
    [SerializeField]
    private float _damage = 10.0f;
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private float _attackSpeed = 10.0f;

    private CharacterStats _stats;

    private void Start()
    {
        GetComponent<Weapon>().WeaponAction = this;
        _stats = GameManager.Instance.Player.GetComponent<CharacterStats>();
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(Resources.Load("Projectile"), _shootPoint.transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Projectile>().Damage = _damage * _stats.DamageMultiplier;
        projectile.GetComponent<Rigidbody>().velocity = _shootPoint.transform.forward * _speed;
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }
}
