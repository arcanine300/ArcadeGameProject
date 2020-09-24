using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public CharacterStats PlayerStats;  

    public float Damage;

    public bool CanHitPlayer = false;

    public bool CanHitEnemy = false;

    public GameObject hitVFX;
    public GameObject enemyHitVFX;
    public GameObject muzzleFlash;

    public bool applyBurnEffect = false;
    private Object effect;
    public Weapon _weapon;

    private void Start()
    {
        if (muzzleFlash != null)
        {
            var muzzleVFX = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
        }
        effect = Resources.Load("Burning Effect");
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal); ;
        Vector3 pos = contact.point;

        if (other.collider.CompareTag("Laser"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider, true);
        }

        if (other.collider.CompareTag("Untagged") || other.collider.CompareTag("MapObject"))
        {
            if (hitVFX != null)
            {
                Instantiate(hitVFX, pos, rot);
            }
            Destroy(gameObject);
        }


        if (CanHitEnemy)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyHealthManager>().RemoveHealth(Damage);

                if (enemyHitVFX != null)
                {
                    Instantiate(enemyHitVFX, pos, rot);
                }

                if (applyBurnEffect)
                {
                    AppliedEffects effects = other.gameObject.GetComponent<AppliedEffects>();
                    if (effects.BurningEffect == null)
                    {
                        effects.BurningEffect = Instantiate(effect, pos, Quaternion.identity) as GameObject;
                        //effects.BurningEffect.GetComponent<EffectDuration>().PlayerStats = _weapon.PlayerStats;
                        effects.BurningEffect.transform.rotation = Quaternion.LookRotation(other.transform.forward);
                        effects.BurningEffect.transform.SetParent(other.transform);
                    }
                }

                Destroy(gameObject);      
            }
        }

        if (CanHitPlayer)
        {
            if (other.collider.CompareTag("Player"))
            {

                if (enemyHitVFX != null)
                {
                    Instantiate(enemyHitVFX, pos, rot);
                }

                other.gameObject.GetComponent<PlayerHealth>().RemoveHealth(Damage);
                Destroy(gameObject);
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
