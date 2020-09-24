using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeImpact : MonoBehaviour
{
    public bool CanHitPlayer = false;
    public bool CanHitEnemy = false;

    public float Damage = 10.0f;

    public bool IsActive = false;

    public GameObject Model;

    public GameObject ExplosionEffect;

    public GameObject Trail;

    public Collider ting;
    public Collider explosionArea;
    public GameObject enemyHitVFX;

    private void OnCollisionEnter(Collision collision)
    {
        Trail.SetActive(false);
        IsActive = true;
        ExplosionEffect.SetActive(true);
        Model.SetActive(false);
        ting.enabled = false;
        explosionArea.enabled = true;
        StartCoroutine(DestroyGrenade());        
    }

    IEnumerator DestroyGrenade()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsActive)
        {           
            if (CanHitPlayer)
            {
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<PlayerHealth>().RemoveHealth(Damage);
                }
            }

            if (CanHitEnemy)
            {
                if (other.CompareTag("Enemy"))
                {
                    if(enemyHitVFX != null)
                    {
                        Instantiate(enemyHitVFX, other.transform.position, other.transform.rotation);
                    }
                    other.gameObject.GetComponent<EnemyHealthManager>().RemoveHealth(Damage);
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (IsActive)
        {
            if (CanHitPlayer)
            {
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<PlayerHealth>().RemoveHealth(Damage);
                }
            }

            if (CanHitEnemy)
            {
                if (other.CompareTag("Enemy"))
                {
                    if (enemyHitVFX != null)
                    {
                        Instantiate(enemyHitVFX, other.transform.position, other.transform.rotation);
                    }
                    other.gameObject.GetComponent<EnemyHealthManager>().RemoveHealth(Damage);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
    }
}
