using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float Damage = 10.0f;

    public float Height = 4.0f;

    public float Distance = 5.0f;

    public void FireGrenade(Transform turret)
    {
        GameObject grenade = Instantiate(Resources.Load(""), turret.position, Quaternion.identity) as GameObject;
    }
}
