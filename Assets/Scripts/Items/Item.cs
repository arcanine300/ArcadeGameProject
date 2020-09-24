using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject dropEffect;

    public GameObject model;

    public float RemainingTime { get; private set; }

    public string Rarity;

    public IItem ItemAction { get; set; }

    [SerializeField]
    private float _activeTime = 10.0f;

    private IEnumerator Countdown()
    {
        RemainingTime = _activeTime;

        while (RemainingTime > 0)
        {
            yield return new WaitForFixedUpdate();
            RemainingTime -= Time.fixedDeltaTime;
        }
        RemainingTime = 0;
        Destroy(gameObject);
    }

    public void StartActivatedTime()
    {
        StartCoroutine(Countdown());
    }
}
