using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour
{
    public bool IgnorePlayer = true;

    public bool IgnoreProjectile = true;

    public bool IgnoreEnemy = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IgnorePlayer)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.CompareTag("Projectile") && !IgnoreProjectile)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.CompareTag("Enemy") && !IgnoreEnemy)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && IgnorePlayer)
    //    {
    //        Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
    //    }

    //    if (collision.gameObject.CompareTag("Projectile") && IgnoreProjectile)
    //    {
    //        Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
    //    }

    //    if (collision.gameObject.CompareTag("Enemy") && IgnoreEnemy)
    //    {
    //        Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
    //    }
    //}
}
