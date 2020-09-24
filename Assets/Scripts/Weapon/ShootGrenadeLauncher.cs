using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGrenadeLauncher : MonoBehaviour, IWeapon
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
    private float _distance = 10.0f;
    [SerializeField]
    private float _delta = 2.0f;

    private CharacterStats _stats;

    private void Start()
    {
        GetComponent<Weapon>().WeaponAction = this;
        _stats = GameManager.Instance.Player.GetComponent<CharacterStats>();
    }

    public void Shoot()
    {
        Vector3 temp = _shootPoint.transform.position;
        temp.y++;
        GameObject grenade = Instantiate(Resources.Load("Grenade"), temp, Quaternion.identity) as GameObject;

        grenade.GetComponent<GrenadeImpact>().CanHitEnemy = true;
        grenade.GetComponent<GrenadeImpact>().CanHitPlayer = false;
        grenade.GetComponent<GrenadeImpact>().Damage = _damage * _stats.DamageMultiplier;

        Vector3 dir = new Vector3(_shootPoint.transform.forward.x, _shootPoint.transform.forward.y, _shootPoint.transform.forward.z);
        //dir.y += .1f;

        grenade.GetComponent<Rigidbody>().velocity = dir * (_distance + Random.Range(-_delta, _delta));
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }
}
