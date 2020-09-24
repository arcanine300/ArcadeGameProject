using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    public float RotateSpeed = 180.0f;

    private void Update()
    {
        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.Self);
    }
}
