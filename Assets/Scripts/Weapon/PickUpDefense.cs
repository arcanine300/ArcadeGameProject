using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDefense : MonoBehaviour
{
    public CharacterStats PlayerStats;

    public CurrentDefense CurrentDefense;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Defense"))
        {
            Defense defense = other.gameObject.GetComponent<Defense>();

            if (defense != null)
            {
                if (!defense.IsActive)
                {
                    defense.StartActivatedTime();
                    defense.PlayerStats = PlayerStats;

                    Destroy(other.GetComponent<Defense>().dropEffect);

                    defense.PlayerController = GetComponent<CharacterController>();
                    //defense.PlayerCollider = GetComponent<CharacterController>();
                    CurrentDefense.SwitchTo(other.gameObject);
                    defense.IsActive = true;
                }                
            }
        }
    }
}
