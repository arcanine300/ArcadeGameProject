using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateTowardsCursor : MonoBehaviour
{
    [SerializeField]
    private ShootWeapon _shootWeaponScript;

    public Transform PlayerModel;

    public float ControllerRotateSpeed = 1.0f;

    public Joystick touchJoystick;

    private PlayerInput inputActions;

    public Animator playerAnim;

    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.PS4.TurnAround.performed += (context) => Look(context.ReadValue<Vector2>());
        inputActions.PS4.Enable();
        inputActions.XboxOne.TurnAround.performed += (context) => Look(context.ReadValue<Vector2>());
        inputActions.XboxOne.Enable();
    }

    private Vector2 mouseCall = new Vector2(100, 100);

    private void Update()
    {
        Look(mouseCall);
    }

    private void Look(Vector2 input)
    {
        // this is for mobile devices
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Vector3 joystickInput = new Vector3(touchJoystick.Horizontal, touchJoystick.Vertical);
            if (playerAnim)
            {
                playerAnim.SetFloat("X", joystickInput.x);
                playerAnim.SetFloat("Y", joystickInput.y);
            }
            PlayerModel.transform.eulerAngles = new Vector3(PlayerModel.eulerAngles.x, Mathf.Atan2(joystickInput.x, joystickInput.y) * Mathf.Rad2Deg, PlayerModel.eulerAngles.z);

            if (joystickInput != Vector3.zero)
            {
                _shootWeaponScript.Shoot();
            }                      
        }
        // this is for pc controls, mos def gotta reowrk the console stuff later when we get to it
        else
        {
            Vector3 pos = Input.mousePosition;
            pos.x -= Screen.width / 2;
            pos.y -= Screen.height / 2;

            float rad = 2;
            float ratio = rad / Mathf.Abs(rad - pos.magnitude);

            Vector3 worldPos = new Vector3(PlayerModel.parent.transform.position.x + pos.x * ratio,
                                           PlayerModel.parent.transform.position.y,
                                           PlayerModel.parent.transform.position.z + pos.y * ratio);
            PlayerModel.LookAt(worldPos);
        }
    }
}