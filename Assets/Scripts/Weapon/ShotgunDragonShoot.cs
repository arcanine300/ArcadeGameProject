using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunDragonShoot : MonoBehaviour, IWeapon
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
    [SerializeField]
    private int pelletCount = 5;

    private CharacterStats _stats;

    private void Start()
    {
        GetComponent<Weapon>().WeaponAction = this;
        _stats = GameManager.Instance.Player.GetComponent<CharacterStats>();
    }

    public void Shoot()
    {
        Vector3 temp = _shootPoint.transform.position;
        temp.y = 1.3f;

        for (int i = 0; i < pelletCount; i++)
        {
            GameObject bullet = Instantiate(Resources.Load("DragonsBreath"), temp, Quaternion.identity) as GameObject;

            Bullet b = bullet.GetComponent<Bullet>();
            b.applyBurnEffect = true;
            b.Damage = _damage * _stats.DamageMultiplier;

            Vector3 dir = _shootPoint.transform.forward;
            dir.x += Random.Range(-_spread, _spread);

            bullet.GetComponent<Rigidbody>().velocity = dir * _speed;
        }
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }
}
