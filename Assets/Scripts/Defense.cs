using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    public CharacterStats PlayerStats;

    public CharacterController PlayerController;

    public GameObject dropEffect;

    public bool IsActive { get; set; }

    public int ActiveTime = 25;

    public string Rarity;

    public float TimeLeft;

    IEnumerator ActivateTimer()
    {
        TimeLeft = ActiveTime;

        while (TimeLeft > 0)
        {
            yield return new WaitForFixedUpdate();
            TimeLeft -= Time.fixedDeltaTime;
        }

        //yield return new WaitForSeconds(ActiveTime);
        Destroy(gameObject);
    }

    public void StartActivatedTime()
    {
        StartCoroutine(ActivateTimer());
    }
}
