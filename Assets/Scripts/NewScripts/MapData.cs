using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyType1;
    [SerializeField]    
    private GameObject _enemyType2;
    [SerializeField]
    private GameObject _enemySpawns;
    [SerializeField]
    private string _mapGimmick;
    [SerializeField]
    private string _mapItemLocation;
    [SerializeField]
    private int _enemyType1Amount;
    [SerializeField]
    private int _enemyType2Amount;
    [SerializeField]
    private int _hordeCount;

    public int GetHordeCount()
    {
        return _hordeCount;
    }

    public GameObject[] GetEnemyTypes()
    {
        return new GameObject[] { _enemyType1, _enemyType2 };
    }

    public int[] GetEnemyTypeAmounts()
    {
        return new int[] { _enemyType1Amount, _enemyType2Amount };
    }

    public GameObject GetEnemySpawnPoints()
    {
        return _enemySpawns;
    }

    public string GetMapGimmickName()
    {
        return _mapGimmick;
    }

    public string GetMapItemLocation()
    {
        return _mapItemLocation;
    }
}
