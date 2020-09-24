using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public bool SpawningEnemies { get; private set; }

    public bool LockState { get; set; }

    [SerializeField]
    private float _enemySpawnGap = 0.5f;
    [SerializeField]
    private int _hordeCount = 5;

    private int _enemyType1Amount;
    private int _enemyType2Amount;
    private GameObject _enemyType1;
    private GameObject _enemyType2;
    private Transform _playerTransform;
    private List<GameObject> _currentEnemies;
    private List<Vector3> _enemySpawnPointPositions;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        MapData mapData = GameObject.Find("Map Data").GetComponent<MapData>();
        GameObject[] mapDataEnemyTypes = mapData.GetEnemyTypes();
        int[] mapDataEnemyTypeAmounts = mapData.GetEnemyTypeAmounts();

        SetMapEnemyData(mapDataEnemyTypes[0], mapDataEnemyTypes[1]);
        SetInitialEnemyTypeAmounts(mapDataEnemyTypeAmounts[0], mapDataEnemyTypeAmounts[1]);
        SetMapEnemySpawnPoints(mapData.GetEnemySpawnPoints());
        _hordeCount = mapData.GetHordeCount();

        _playerTransform = GameManager.Instance.Player.transform;

        LockState = false;

        if (_playerTransform)
        {
            StartCoroutine(StartDelay());
        }
    }

    private IEnumerator StartDelay()
    {
        if (_playerTransform)
        {
            yield return new WaitForSeconds(2.0f);
            GameManager.Instance.StartWave();
        }
    }

    private void SetInitialEnemyTypeAmounts(int x, int y)
    {
        _enemyType1Amount = x;
        _enemyType2Amount = y;
    }

    private void SetMapEnemyData(GameObject enemyType1, GameObject enemyType2)
    {
        _enemyType1 = enemyType1;
        Debug.Log($"Set enemy type 1 as {enemyType1.name}");
        _enemyType2 = enemyType2;
        Debug.Log($"Set enemy type 2 as {enemyType2.name}");
    }

    private void SetMapEnemySpawnPoints(GameObject spawnPoints)
    {
        _enemySpawnPointPositions = new List<Vector3>();

        Debug.Log("Setting enemy spawn points.");
        foreach (Transform child in spawnPoints.transform)
        {
            _enemySpawnPointPositions.Add(child.position);
            //Debug.Log("Set enemy spawn point.");
        }
    }

    private void AddEnemy(GameObject enemyPrefab, Vector3 spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

        enemy.transform.SetParent(transform);
        _currentEnemies.Add(enemy);

        enemy.GetComponent<EnemyMoveTo>().Target = _playerTransform;
    }

    private IEnumerator SpawnMeleeHordes(int enemyCount)
    {
        Vector3 spawnPoint = GetRandomNewSpawnPoint();        
        while (enemyCount > 0)
        {
            for (int i = 0; i < _hordeCount; i++)
            {
                if (enemyCount <= 0)
                {
                    break;
                }
                AddEnemy(_enemyType1, spawnPoint);
                enemyCount--;
                yield return new WaitForSeconds(Random.Range(0, 0.6f));
            }
            yield return new WaitForSeconds(Random.Range(0, 1f));
            spawnPoint = GetRandomNewSpawnPoint();
        }
    }

    private Vector3 _previousSpawnPoint;
    private Vector3 GetRandomNewSpawnPoint()
    {
        Vector3 spawnPoint = _enemySpawnPointPositions[Random.Range(0, _enemySpawnPointPositions.Count)];

        while (true)
        {
            if (spawnPoint != _previousSpawnPoint || _enemySpawnPointPositions.Count <= 1)
            {
                _previousSpawnPoint = spawnPoint;
                return spawnPoint;
            }
            else
            {
                spawnPoint = _enemySpawnPointPositions[Random.Range(0, _enemySpawnPointPositions.Count)];
            }
        }
    }

    public void StartWave()
    {
        _currentEnemies = new List<GameObject>();
        switch (GameManager.Instance.CurrentMapWave)
        {
            case 1:
                StartCoroutine(SpawnMeleeHordes(15));
                break;
            case 2:
                StartCoroutine(SpawnMeleeHordes(30));
                _hordeCount += 2;
                break;
            case 3:
                StartCoroutine(SpawnMeleeHordes(50));
                _hordeCount += 5;
                break;
            case 4:
                StartCoroutine(SpawnMeleeHordes(80));
                _hordeCount += 10;
                break;
            default:
                break;
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            _currentEnemies.Remove(enemy);

            if (_currentEnemies.Count <= 0 && !SpawningEnemies)
            {
                if (!LockState)
                {
                    StartCoroutine(DelayEndWave());                    
                }                
            }
        }
    }

    private IEnumerator DelayEndWave()
    {
        LockState = true;
        GameManager.Instance.EndWave();
        yield return new WaitForSeconds(1.0f);
        LockState = false;
    }

    public Vector3 GetClosestEnemyPosition(Transform pos)
    {
        Vector3 closestPos = _currentEnemies[0].transform.position;

        foreach (GameObject enemy in _currentEnemies)
        {
            if (Vector3.Distance(pos.position, enemy.transform.position) < closestPos.magnitude)
            {
                closestPos = enemy.transform.position;
            }
        }

        return closestPos;
    }
}