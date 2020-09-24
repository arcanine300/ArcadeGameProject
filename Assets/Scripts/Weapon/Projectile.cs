using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionVFX;

    public float Damage;

    public float Speed;

    public int HitsLeft;

    public bool CanHitPlayer = false;

    public bool CanHitEnemy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapObject"))
        {
            if (_explosionVFX)
            {
                Instantiate(_explosionVFX, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        if (CanHitEnemy)
        {
            if (other.CompareTag("Enemy"))
            {
                HitsLeft--;

                other.gameObject.GetComponent<EnemyHealthManager>().RemoveHealth(Damage);

                if (HitsLeft == 0)
                {
                    if (_explosionVFX)
                    {
                        Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject);
                }
            }
        }

        if (CanHitPlayer)
        {
            if (other.CompareTag("Player"))
            {
                HitsLeft--;

                other.gameObject.GetComponent<PlayerHealth>().RemoveHealth(Damage);

                if (HitsLeft == 0)
                {
                    if (_explosionVFX)
                    {
                        Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            if (_explosionVFX)
            {
                Instantiate(_explosionVFX, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
