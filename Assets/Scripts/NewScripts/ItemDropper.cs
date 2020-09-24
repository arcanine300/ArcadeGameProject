using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public static ItemDropper Instance { get; private set; }

    public float BaseDrop { get { return _baseDrop; } }
    public float DropMultiplier { get { return _dropMultiplier; } }

    [SerializeField]
    private float _dropMultiplierAmount = 0.5f;
    [SerializeField]
    private float _baseDrop = 1.0f;
    [SerializeField]
    private float _dropMultiplier = 0.0f;
    [SerializeField]
    private float _spawnDistance = 15.0f;

    private string _mapItem;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ResetDropMultiplier();
    }

    public void GetMapItem(string mapItem)
    {
        _mapItem = mapItem;
    }

    public void ResetDropMultiplier()
    {
        _dropMultiplier = 0;
    }

    public void IncreaseDropMultiplier()
    {
        _dropMultiplier += _dropMultiplierAmount;
    }

    public void EnemyDropChance(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            float baseDrop = BaseDrop;
            float dropMultiplier = DropMultiplier;

            if (Random.Range(0f, 100f) < baseDrop * dropMultiplier)
            {
                string path = string.Empty;
                int a = Random.Range(0, 100);
                int x = Random.Range(0, 100);

                // weapons
                if (a >= 60 && a <= 100)
                {
                    if (x >= 75 && x <= 100)
                        path = Refrences.Instance.GetRandomWeapon("green");
                    if (x >= 50 && x < 75)
                        path = Refrences.Instance.GetRandomWeapon("blue");
                    if (x >= 25 && x < 50)
                        path = Refrences.Instance.GetRandomWeapon("purple");
                    if (x >= 0 && x < 25)
                        path = Refrences.Instance.GetRandomWeapon("red");
                    if (path == string.Empty)
                        path = Refrences.Instance.GetRandomWeapon("green");
                }
                // defenses
                else if (a >= 30 && a < 60)
                {
                    if (x >= 45 && x <= 100)
                        path = Refrences.Instance.GetRandomDefense("green");
                    if (x >= 15 && x < 45)
                        path = Refrences.Instance.GetRandomDefense("blue");
                    if (x >= 5 && x < 15)
                        path = Refrences.Instance.GetRandomDefense("puple");
                    if (x >= 0 && x < 5)
                        path = Refrences.Instance.GetRandomDefense("red");
                    if (path == string.Empty)
                        path = Refrences.Instance.GetRandomDefense("blue");
                }
                // items
                else if (a >= 0 && a < 30)
                {
                    if (x >= 45 && x <= 100)
                        path = Refrences.Instance.GetRandomItem("green");
                    if (x >= 35 && x < 45)
                        path = _mapItem;
                    if (x >= 15 && x < 35)
                        path = Refrences.Instance.GetRandomItem("blue");
                    if (x >= 5 && x < 15)
                        path = Refrences.Instance.GetRandomItem("puple");
                    if (x >= 0 && x < 5)
                        path = Refrences.Instance.GetRandomItem("red");
                    if (path == string.Empty)
                        path = Refrences.Instance.GetRandomItem("purple");
                }
                //particle effects here
                Vector3 spawnPos = new Vector3(enemy.transform.position.x, 1.5f, enemy.transform.position.z);
                //Vector3 spawnPos = new Vector3(GameManager.Instance.Player.transform.position.x + Random.Range(-_spawnDistance, _spawnDistance + 1), 
                  //                             1.5f, 
                    //                           GameManager.Instance.Player.transform.position.z + Random.Range(-_spawnDistance, _spawnDistance + 1));
                //Debug.Log($"item to drop: {path}");
                Object spawnObj = Resources.Load(path);

                if (spawnObj != null)
                {
                    _ = Instantiate(spawnObj, spawnPos, Quaternion.identity) as GameObject;
                }               
                ResetDropMultiplier();
            }
            else
            {
                IncreaseDropMultiplier();
            }
        }
    }
}
