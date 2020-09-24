using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBounce : MonoBehaviour
{
    private Rigidbody _rb;

    private Vector3 _lastFrameVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lastFrameVelocity = _rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        float speed = _lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(_lastFrameVelocity.normalized, collisionNormal);
        _rb.velocity = direction * speed;
    }
}
