using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGimmick : MonoBehaviour
{
    private void Start()
    {        
        GimmickController.Instance.GetMapGimmick();
        ItemDropper.Instance.GetMapItem(GetComponent<MapData>().GetMapItemLocation());
    }
}
