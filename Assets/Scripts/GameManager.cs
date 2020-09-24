using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameObject Player { get; private set; }
    public float BreakTimeLeft { get; private set; }
    public float PlayerScore { get; private set; }
    public int CurrentGameWave { get; private set; }
    public int CurrentMapWave { get; private set; }
    public int KillStreak { get; set; }

    public Transform PlayerSpawnPoint;

    public Animator UIAnimator;

    private MapLoader _mapLoader;
    private string _mapToLoad; // new member variable that determines what level is loaded in OnFadeOutComplete()
    private bool _spawnPlayerOnMapLoad; // Set this before calling UIAnimator.SetBool("FadeStart", true); if you want to spawn the player
                                        // on the scene that you are loading into
    private float _scoreClump;
    /*
    private PlayerMove _playerMove;
    private RotateTowardsCursor _playerRotate;
    private CharacterController _playerController;
    */

    [SerializeField]
    private float _waveBreakTime = 10.0f;
    [SerializeField]
    private float _scoreMultiplier = 1.0f;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private int _requiredKillStreakAmount = 10;
    [SerializeField]
    private float _requiredScoreClump = 10000;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; //set delegate to run everytime a scene is loaded
    }

    private void Start()
    {
        CurrentGameWave = 0;
        CurrentMapWave = 0;
        _spawnPlayerOnMapLoad = false;

        _mapLoader = GetComponent<MapLoader>();
        _mapLoader.SetMapNames();        
    }

    public void LoadRandomMap()
    {
        if (_mapLoader.MapsLeft > 0)
        {
            //Debug.Log($"maps left: {_mapLoader.MapsLeft}");
            string mapName = _mapLoader.GetRandomMapName();
            
            mapName = mapName == string.Empty ? "Menu" : mapName;
            Debug.Log($"loading map: {mapName}");

            //This is how you handle loading a new level, the animaton controller for the Screen Fade Canvas has an event that calls OnFadeOutComplete() 
            //when the screen is fully black and then loads the next scene.
            _mapToLoad = mapName;
            _spawnPlayerOnMapLoad = true;
            UIAnimator.SetBool("FadeStart", true);
            //SceneManager.LoadScene(mapName);

            //Debug.Log($"loaded map: {mapName}");
        }
        else
        {
            // This is how you handle loading a new level, the animaton controller for the Screen Fade Canvas has an event that calls OnFadeOutComplete()
            //when the screen is fully black and then loads the next scene.
            _mapToLoad = "end_game";
            UIAnimator.SetBool("FadeStart", true);
            //SceneManager.LoadScene("end_game");

            //Destroy(gameObject);
        }
    }

    //Function runs at the end of the "Fade_Start" animation using an animation event
    public void OnFadeOutComplete()
    {
        //Debug.Log("Spawned Player: " + _spawnPlayerOnMapLoad);
        UIAnimator.SetBool("FadeStart", false);
        SceneManager.LoadScene(_mapToLoad); //loads the level stored in the gm
    }

    //Function runs at the end of the "Fade_End" animation using an animation event
    public void OnFadeInComplete() { UIAnimator.SetBool("FadeEnd", false); }

    //runs everytime a scene is loaded
    void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        if (_mapToLoad == "end_game")
        {
            Destroy(Player);
        }
        else if (Player == null && _spawnPlayerOnMapLoad == true)
        {
            //spawn at 0.6f on y-axis because the center of the  player is 0.5f, so  it needs to be offset to spawn ontop of the plane at 0,0,0.
            Player = Instantiate(_playerPrefab, PlayerSpawnPoint.position, Quaternion.identity);
            _spawnPlayerOnMapLoad = false; //reset bool after spawning player
        }
        else if (Player != null)
        {
            Player.GetComponent<CharacterStats>().MoveSpeed = Player.GetComponent<CharacterStats>().DefaultPlayerMoveSpeed;
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerSpawnPoint.position;
            Player.GetComponent<CharacterController>().enabled = true;
            _spawnPlayerOnMapLoad = false;
        }

        UIAnimator.SetBool("FadeEnd", true);
    }

    public void LoadMenu()
    {
        //Destroy(Player);

        // This is how you handle loading a new level, the animaton controller for the Screen Fade Canvas has an event that calls OnFadeOutComplete()
        //when the screen is fully black and then loads the next scene.
        _mapToLoad = "Menu";
        UIAnimator.SetBool("FadeStart", true);
        //SceneManager.LoadScene("Menu");
    }

    public void EndGame()
    {
        // This is how you handle loading a new level, the animaton controller for the Screen Fade Canvas has an event that calls OnFadeOutComplete()
        //when the screen is fully black and then loads the next scene.
        _mapToLoad = "end_game";
        UIAnimator.SetBool("FadeStart", true);
        
        //StartCoroutine(LoadLevel("end_game"));
    }

    public void StartWave()
    {
        CurrentGameWave++;
        CurrentMapWave++;
        Debug.Log($"started map wave {CurrentMapWave}, game wave {CurrentGameWave}");

        if (GimmickController.Instance.MapGimmick != null)
        {
            GimmickController.Instance.StartGimmick();
        }

        //GimmickController.Instance.StartGimmick();
        EnemyManager.Instance.StartWave();
    }

    public void EndWave()
    {
        GimmickController.Instance.StopGimmick();
        if (CurrentMapWave < 4)
        {
            StartCoroutine(StartWaveBreak());
        }
        else
        {
            // this is where we switch maps or end the run.
            CurrentMapWave = 0;
            Debug.Log("map ended");
            LoadRandomMap();
        }
    }

    private IEnumerator StartWaveBreak()
    {
        BreakTimeLeft = _waveBreakTime;

        while (BreakTimeLeft > 0)
        {
            yield return new WaitForSeconds(0.1f);
            BreakTimeLeft -= 0.1f;
        }
        BreakTimeLeft = 0;

        if (CurrentMapWave < 4)
        {
            StartWave();
        }
    }
   
    // work in progess
    public void IncreaseScore(float amount)
    {        
        KillStreak++;
        if (KillStreak == _requiredKillStreakAmount)
        {
            Player.GetComponent<CharacterStats>().ScoreMultiplier += 0.25f;
            KillStreak = 0;
        }
        PlayerScore += amount * Player.GetComponent<CharacterStats>().ScoreMultiplier;

        // adds an extra life after every x amount of score earned.
        _scoreClump += amount * Player.GetComponent<CharacterStats>().ScoreMultiplier;
        if (_scoreClump >= _requiredScoreClump)
        {
            Player.GetComponent<PlayerHealth>().Lives++;
            _scoreClump = 0;
        }
    }

    public void RemoveMultiplier()
    {
        float temp = Player.GetComponent<CharacterStats>().ScoreMultiplier - 1;
        if (temp >= 1)
        {
            Player.GetComponent<CharacterStats>().ScoreMultiplier -= 1;
        }
        else
        {
            Player.GetComponent<CharacterStats>().ScoreMultiplier = 1;
        }
    }
}
