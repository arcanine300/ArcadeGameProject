using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDefense : MonoBehaviour
{
    public Transform PlayerModel;

    public CharacterStats PlayerStats;

    public GameObject Defense;// { get; private set; }

    public float ActiveTime = 25;

    public float TimeLeft = 0;

    public float interval = 0.01f;    

    public void SwitchTo(GameObject defense)
    {
        if (defense.CompareTag("Defense"))
        {
            Destroy(Defense);
            Defense = defense;            
            Defense.transform.SetParent(transform);
            defense.transform.localPosition = Vector3.zero;
        }
    }
}
