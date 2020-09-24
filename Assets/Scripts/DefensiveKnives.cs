using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveKnives : MonoBehaviour
{
    private Defense _defense;

    public GameObject Knife1;

    public GameObject Knife2;

    public float RotateSpeed = 360.0f;

    public float Radius = 4.0f;

    private bool _activated = false;

    private Transform PlayerTransform;

    private void Start()
    {
        _defense = GetComponent<Defense>();
    }

    private void Update()
    {       
        if (_defense.IsActive && !_activated)
        {
            PlayerTransform = _defense.PlayerController.transform;
            Knife1.transform.localPosition = new Vector3(0, 0, Radius);
            Knife2.transform.localPosition = new Vector3(0, 0, -Radius);

            Knife1.GetComponent<Knife>().PlayerStats = _defense.PlayerStats;
            Knife2.GetComponent<Knife>().PlayerStats = _defense.PlayerStats;

            Knife1.GetComponent<SphereCollider>().enabled = true;
            Knife2.GetComponent<SphereCollider>().enabled = true;

            Knife1.transform.localRotation = Quaternion.Euler(90, 0, 90);
            Knife2.transform.localRotation = Quaternion.Euler(90, 0, 90);
            transform.SetParent(null);

            _activated = true;

            transform.position = PlayerTransform.position;
            transform.SetParent(PlayerTransform);
        }

        if (_activated)
        {
            Knife1.transform.RotateAround(transform.position, new Vector3(0, 1, 0), RotateSpeed * Time.deltaTime);

            Knife2.transform.RotateAround(transform.position, new Vector3(0, 1, 0), RotateSpeed * Time.deltaTime);

            transform.position = PlayerTransform.position;
        }
    }
}
