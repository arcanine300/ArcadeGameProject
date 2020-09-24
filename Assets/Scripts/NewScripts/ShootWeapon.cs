using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform _playerModel;

    [SerializeField]
    private GameObject _defaultWeaponPrefab;

    private PlayerInput _playerInput;

    public GameObject _currentWeapon;

    public Vector3 localGunPos;

    private Weapon _currentWeaponScript;
    private bool _isDelay;

    private CharacterStats _stats;

    public bool IsShooting { get; private set; }

    public GameObject CurrentWeapon
    {
        get
        {
            return _currentWeapon;
        }

        set
        {
            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon);
            }

            _currentWeapon = value;
            _currentWeaponScript = _currentWeapon.GetComponent<Weapon>();
            _currentWeapon.transform.SetParent(_playerModel);
            _currentWeaponScript.DisableModel();
            _currentWeapon.transform.rotation = _playerModel.transform.rotation;
            _currentWeapon.transform.localPosition = localGunPos;
            _currentWeapon.GetComponent<BoxCollider>().enabled = false;

            if (_currentWeaponScript.RarityEffect != null)
            {
                _currentWeaponScript.RarityEffect.SetActive(false);
            }

            if (_currentWeapon.gameObject.GetComponent<SpinWeapon>() != null)
            {
                _currentWeapon.gameObject.GetComponent<SpinWeapon>().enabled = false;
            }
        }
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.PS4.TurnAround.started += (context) => ShootCallback(context);
        _playerInput.PS4.Enable();
        _playerInput.XboxOne.TurnAround.started += (context) => ShootCallback(context);
        _playerInput.XboxOne.Enable();
        _playerInput.PC.Shoot.started += (context) => ShootCallback(context);
        _playerInput.PC.Enable();

        _isDelay = false;
        IsShooting = false;
        _stats = transform.parent.GetComponent<CharacterStats>();

        UseDefault();
    }

    public void UseDefault()
    {
        CurrentWeapon = Instantiate(_defaultWeaponPrefab, _playerModel.position, Quaternion.identity);
    }

    private IEnumerator StartShoot()
    {
        _isDelay = true;
        IsShooting = true;
        _currentWeaponScript.Shoot();

        float speed = _currentWeaponScript.WeaponAction.GetAttackSpeed();

        if (speed == 0)
        {
            yield return new WaitForSeconds(0);
        }
        else
        {
            yield return new WaitForSeconds(1 / (_currentWeaponScript.WeaponAction.GetAttackSpeed() * _stats.AttackSpeed));
        }
        IsShooting = false;
        _isDelay = false;
    }

    private void ShootCallback(InputAction.CallbackContext context)
    {
        if (!_isDelay)
        {
            StartCoroutine(StartShoot());
        }
    }

    public void Shoot()
    {
        if (!_isDelay)
        {
            StartCoroutine(StartShoot());
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed == true)
        //if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {            
            Shoot();
        }
    }
}
