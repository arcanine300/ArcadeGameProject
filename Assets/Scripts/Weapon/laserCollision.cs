using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserCollision : MonoBehaviour
{
    public float Damage;
    public int enemyPen;
    private int currentPen;
    //public Weapon Weapon;
    private Object effect;

    private LineRenderer lr;

    private GameObject _lastburningEffect;
    

    void Start()
    {
        effect = Resources.Load("Burning Effect");
        lr = GetComponent<LineRenderer>();
        currentPen = enemyPen;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lr.SetPosition(0,transform.position);
        lr.SetPosition(1, transform.position + (transform.forward * 5000));
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 500f);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider)
            {
                //Debug.Log(i + ", " + hits[i].transform.name);
                if (hits[i].transform.CompareTag("Enemy"))
                {
                    if (currentPen > 0)
                    {
                        currentPen -= 1;
                        AppliedEffects effects = hits[i].transform.GetComponent<AppliedEffects>();

                        if (effects.BurningEffect == null)
                        {
                            effects.BurningEffect = Instantiate(effect, hits[i].point, Quaternion.identity) as GameObject;
                            //sweffects.BurningEffect.GetComponent<EffectDuration>().PlayerStats = Weapon.PlayerStats;
                            effects.BurningEffect.transform.rotation = Quaternion.LookRotation(hits[i].transform.forward);
                            effects.BurningEffect.transform.SetParent(hits[i].transform);
                        }

                        hits[i].transform.GetComponent<EnemyHealthManager>().RemoveHealth(Damage * Time.deltaTime);
                    }
                    else
                    {
                        currentPen = enemyPen;
                        break;
                    }
                }
                else if (!hits[i].transform.CompareTag("Weapon"))
                {
                    lr.SetPosition(1, hits[i].point);
                    currentPen = enemyPen;  
                }
            }else
            {
                currentPen = enemyPen;
            }
        }
    }
}
