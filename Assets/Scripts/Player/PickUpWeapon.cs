using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public Transform PlayerModel;

    public CharacterStats PlayerStats;

    public GameObject ReviveBlast;

    public Vector3 weaponLocalPos;

    [SerializeField]
    private ShootWeapon _shootWeaponScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && GameObject.Find("Blast Radius") == null)
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();

            if (weapon != null)
            {
                other.gameObject.GetComponent<Weapon>().DisableModel();
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                weapon.gameObject.GetComponent<SpinWeapon>().enabled = false;
                other.transform.rotation = _shootWeaponScript.transform.rotation;
                other.transform.localPosition = weaponLocalPos;
                other.gameObject.GetComponent<Weapon>().StartTimer();
                _shootWeaponScript.CurrentWeapon = other.gameObject;
            }
        }
    }
}
