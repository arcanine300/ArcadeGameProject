using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GimmickController : MonoBehaviour
{
    public static GimmickController Instance { get; private set; }

    public IMapGimmick MapGimmick { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);        
    }

    public void GetMapGimmick()
    {
        // this is pretty much needed cuz it wont find the map data otherwise. cuz gameobject.find broke when map gimmick is an instance. idk
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        GameObject mapDataObj = null;

        foreach (var gameObj in rootGameObjects)
        {
            if (gameObj.name == "Map Data")
            {
                mapDataObj = gameObj;
            }
        }
       
        if (mapDataObj != null)
        {
            MapData mapData = mapDataObj.GetComponent<MapData>();

            switch (mapData.GetMapGimmickName())
            {
                case "towers":
                    MapGimmick = GameObject.Find("Tower Controller").GetComponent<TowerController>();
                    break;
                case "lasers":
                    MapGimmick = GameObject.Find("Laser Controller").GetComponent<LaserController>();
                    break;
                case "artillery":
                    MapGimmick = GameObject.Find("Artillery Controller").GetComponent<ArtilleryController>();
                    break;
                case "abstract":
                    MapGimmick = GameObject.Find("Block Controller").GetComponent<BlockController>();
                    break;
                case "snow":
                    MapGimmick = GameObject.Find("Snow Controller").GetComponent<SnowStormGimmick>();
                    break;
                default:
                    MapGimmick = null;
                    break;
            }
        }
        else
        {
            Debug.Log("no map data.");
        }        
    }

    public void StartGimmick()
    {
        MapGimmick.Begin();
    }

    public void StopGimmick()
    {
        MapGimmick.Stop();
    }

    public void UseItem()
    {
        MapGimmick.UseItem();
    }
}
