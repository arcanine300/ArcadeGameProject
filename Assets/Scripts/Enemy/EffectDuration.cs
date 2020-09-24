using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDuration : MonoBehaviour
{
    public float totalTicks = 20f;
    public float tickDelay = 0.1f;
    public float burnDmg = 5.5f;

    public CharacterStats PlayerStats;

    private EnemyHealthManager enemyHp;

    IEnumerator CountDown()
    {
        while (totalTicks > 0)
        {
            totalTicks -= 1;
            enemyHp.RemoveHealth(burnDmg);
            yield return new WaitForSeconds(tickDelay);
        }
        Destroy(gameObject);
        yield return null;
    }

    private void Start()
    {
        enemyHp = transform.parent.GetComponent<EnemyHealthManager>();
        if (enemyHp)
        {
            StartCoroutine(CountDown());
        }
    }
}
