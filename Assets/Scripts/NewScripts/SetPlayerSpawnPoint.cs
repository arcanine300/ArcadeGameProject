using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.PlayerSpawnPoint = transform;
    }
}
