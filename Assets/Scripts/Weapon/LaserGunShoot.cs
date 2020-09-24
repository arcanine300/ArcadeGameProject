using System.Collections;
using UnityEngine;

public class LaserGunShoot : MonoBehaviour, IWeapon
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private int _penetration;

    public Weapon _weapon;

    private GameObject _laser;

    public GameObject _shootPoint;

    private CharacterStats _stats;

    private void Start()
    {
        _stats = GameManager.Instance.Player.GetComponent<CharacterStats>();
        _weapon = GetComponent<Weapon>();
        _laser = Instantiate(Resources.Load("Laser"), _shootPoint.transform.position, Quaternion.identity) as GameObject;
        var _laserCollison =_laser.GetComponent<laserCollision>();
        _laserCollison.Damage = _damage * _stats.DamageMultiplier;
        _laserCollison.enemyPen = _penetration;
        _laser.transform.rotation = Quaternion.LookRotation(_shootPoint.transform.forward);
        _laser.transform.SetParent(transform);
        _laser.SetActive(false);
        GetComponent<Weapon>().WeaponAction = this;        
    }

    public void Shoot()
    {
        if (_weapon.GetShootingValue())
        {
            _laser.SetActive(true);
        }
    }

    private void Update()
    {
        if (!_weapon.GetShootingValue())
        {
            _laser.SetActive(false);
        }
        _laser.transform.rotation = Quaternion.LookRotation(_shootPoint.transform.forward);
    }

    public float GetAttackSpeed()
    {
        return 0;
    }
}
