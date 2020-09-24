using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public CharacterStats PlayerStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item") && GameObject.Find("Blast Radius") == null)
        {
            Item item = other.gameObject.GetComponent<Item>();

            Destroy(item.dropEffect);

            if (item.model != null)
            {
                item.model.SetActive(false);
            }

            item.GetComponent<Collider>().enabled = false;
            item.ItemAction.Activate(gameObject);
            item.StartActivatedTime();            
        }
    }
}
