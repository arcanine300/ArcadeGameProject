using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private GameObject _rarityEffect;
    [SerializeField]
    private ShootWeapon _shootWeaponScript;

    public GameObject RarityEffect { get { return _rarityEffect; } }
    public IWeapon WeaponAction { get; set; }

    [SerializeField]
    private float _activeTime = 10.0f;

    public float RemainingTime { get; private set; }

    private void Start()
    {
        _shootWeaponScript = GameManager.Instance.Player.GetComponent<PlayerParts>().ShootWeapon; //GameObject.Find("Input Controller").GetComponent<ShootWeapon>();
    }

    public void Shoot()
    {
        WeaponAction.Shoot();
    }

    public void DisableModel()
    {
        _model.SetActive(false);
    }

    private IEnumerator Countdown()
    {
        RemainingTime = _activeTime;

        while (RemainingTime > 0)
        {
            yield return new WaitForFixedUpdate();
            RemainingTime -= Time.fixedDeltaTime;
        }
        RemainingTime = 0;
        _shootWeaponScript.UseDefault();
    }

    public bool GetShootingValue()
    {
        return _shootWeaponScript.IsShooting;
    }

    public void StartTimer()
    {
        StartCoroutine(Countdown());
    }

    public void ResetTimer()
    {
        RemainingTime = _activeTime;
    }
}
