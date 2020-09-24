using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfMap : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            transform.GetComponent<CharacterController>().enabled = false;
            transform.position = new Vector3(0, 1.25f, 0);
            GameManager.Instance.Player.GetComponent<PlayerHealth>().Kill();
            transform.GetComponent<CharacterController>().enabled = true;
        }
    }
}
