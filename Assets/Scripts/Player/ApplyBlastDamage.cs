using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBlastDamage : MonoBehaviour
{
    public float DAMAGE = 1000f;

    private CharacterStats playerStats;
    private Collider[] enemies;

    public bool Active = false;

    private void Start()
    {
        playerStats = transform.parent.gameObject.GetComponent<CharacterStats>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(slowSpawn(other));
        }
    }

    IEnumerator slowSpawn(Collider other)
    {
        other.GetComponent<EnemyHealthManager>().RemoveHealth(DAMAGE);
        yield return new WaitForSeconds(0.002f);
    }
}
