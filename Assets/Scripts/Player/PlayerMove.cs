using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform PlayerTransform;

    public CharacterController PlayerCharacterController;

    public Joystick joystick;

    [SerializeField]
    private float _speed = 1.0f;

    public float Gravity = -9.81f;

    public Transform GroundChecker;

    public float GroundDistance = 0.4f;

    public LayerMask GroundMask;

    private Vector3 _velocity;

    private bool _isGrounded;

    public Animator animator;

    public bool temp;

    private CharacterStats _playerStats;

    private void Start()
    {
        _playerStats = GameManager.Instance.Player.GetComponent<CharacterStats>();
    }

    private void LateUpdate()
    {
        Vector3 input = Vector3.zero;

        _isGrounded = Physics.CheckSphere(GroundChecker.position, GroundDistance, GroundMask);

        if (_isGrounded)
        {
            _velocity.y = -2f;
        }

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.z = Input.GetAxisRaw("Vertical");
            if (animator)
            {
                animator.SetFloat("X", input.x);
                animator.SetFloat("Y", input.z);
            }  
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            input.x = joystick.Horizontal * 1.15f;
            input.z = joystick.Vertical * 1.15f;
            if (animator)
            {
                animator.SetFloat("X", input.x);
                animator.SetFloat("Y", input.z);
            }
        }

        //Vector3 move = PlayerTransform.right * input.x + PlayerTransform.forward * input.z;

        //PlayerCharacterController.Move(move * _playerStats.MoveSpeed * Time.deltaTime);
        PlayerCharacterController.Move(Vector3.ClampMagnitude((PlayerTransform.right * input.x) + (PlayerTransform.forward * input.z), 1.0f) * _playerStats.MoveSpeed * Time.deltaTime);
        if (!_isGrounded)
        {
            _velocity.y += Gravity * Time.deltaTime;
        }
        PlayerCharacterController.Move(_velocity * Time.deltaTime);
    }
}
