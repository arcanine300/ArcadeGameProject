using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerHealth hpMan = other.gameObject.GetComponent<PlayerHealth>();

            if (hpMan.Lives > 0)
            {
                if (_explosionVFX)
                {
                    Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                }
                hpMan.Kill();
            }
        }
    }
}
