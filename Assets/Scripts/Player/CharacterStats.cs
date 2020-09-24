using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float DamageMultiplier { get { return _damageMultiplier; }  set { _damageMultiplier = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float ScoreMultiplier { get { return _scoreMultiplier; } set { _scoreMultiplier = value; } }

    [SerializeField]
    private float _damageMultiplier = 1.0f;
    [SerializeField]
    private float _attackSpeed = 1.0f;
    [SerializeField]
    private float _moveSpeed = 5.0f;
    [SerializeField]
    private float _scoreMultiplier = 1.0f;

    public float DefaultPlayerMoveSpeed;

    private void Start()
    {
        DefaultPlayerMoveSpeed = _moveSpeed;
    }
}
