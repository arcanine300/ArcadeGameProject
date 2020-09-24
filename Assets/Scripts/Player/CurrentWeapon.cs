using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CurrentWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform _playerModel;

    public GameObject CurrentWeaponPrefab;

    public GameObject Weapon;

    [SerializeField]
    private Weapon _weapon;

    public PlayerInput PlayerInput;

    public void SwitchTo(GameObject weapon)
    {
        if (weapon.CompareTag("Weapon"))
        {
            Destroy(Weapon);
            Weapon = weapon;
            _weapon = Weapon.GetComponent<Weapon>();
            Weapon.transform.SetParent(transform);
        }
    }

    private void Awake()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.PS4.TurnAround.performed += (context) => Shoot(context);
        PlayerInput.PS4.Enable();
        PlayerInput.XboxOne.TurnAround.performed += (context) => Shoot(context);
        PlayerInput.XboxOne.Enable();
        PlayerInput.PC.Shoot.performed += (context) => Shoot(context);
        PlayerInput.PC.Enable();

        _playerModel = GetComponent<Transform>();

        UseDefault();
    }

    public void UseDefault()
    {
        if (Weapon == null)
        {
            Weapon = Instantiate(Resources.Load("Weapons/Assault-Rifle"), _playerModel.position, Quaternion.identity) as GameObject;
            Weapon.gameObject.GetComponent<SpinWeapon>().enabled = false;
            Weapon.transform.SetParent(transform);
            Weapon.gameObject.GetComponent<Weapon>().DisableModel();
            //Destroy(Weapon.gameObject.GetComponent<Weapon>().dropEffect);

            Weapon.transform.rotation = transform.rotation;
            Weapon.transform.localPosition = Vector3.zero;
            _weapon = Weapon.GetComponent<Weapon>();
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        _weapon.Shoot();
    }
}
