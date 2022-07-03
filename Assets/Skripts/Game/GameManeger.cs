using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private SpawnPoint[] SpawnPoints;

    [Header("Settings to Pool for Enemy")]
    [SerializeField] private Enemy EnemyPrefab;
    [SerializeField] private int SizePoolEnemy ;
    [SerializeField] private bool AftoIncreasePool;

    [Header("Setings Difficulty")]
    [SerializeField] private float DifficultyStartGame = 0;

    [Tooltip("На сколько увеличиваеться сложнасть за у секунду")]
    [SerializeField] private float DifficultyUp = 0.01f;


    [Header("Setings Button for Game")]


    [SerializeField] private Button[] ButtonsForGame;

    [Header("Setings Boosts")]
    [SerializeField] private float TimeSpawnFreeze = 3;
    [SerializeField] private float CoolDownSpawnBoost = 10;
    [SerializeField] private GameObject TriggerZoneDead;
    [SerializeField] private GameObject TriggerZoneSlowdown;
    [SerializeField] private PlayerController playerController;

    [Header("Game Menu Setings")]
    [SerializeField] private Button ButtonRestart;
    [SerializeField] private Button ButtonGpToMainMenu;
    [SerializeField] private Text NewRecord;
    [SerializeField] private Text ThisRecord;
    [SerializeField] private int IndexSceneManeMenu = 0;

    [Header("Setings GameManeger")]
    [SerializeField] private string KeyPlayerPrefsRecords = "myrecord";
    [SerializeField] private string KeyMusicSoundValue = "musicvalue";
    [SerializeField] private HPWalls hpWals;
    [SerializeField] private GameObject CanvasGame;
    [SerializeField] private GameObject CanvasMenu;
    [SerializeField] private AudioSource MusicPlayer;
    [SerializeField] private Text ThisRecordGame;

    private PoolGameObjects<Enemy> PoolEnemy;
    private bool ActiveSpawn = false;
    private bool ActiveGame = false;
    private bool ActiveTimer = false;
    private bool SpawnFreeze = false;
    private int TimeInGame = 0;
    private float DifficultyWave = 1;
    private bool[] CoolDownBoostsActive;


    // Start is call ed before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey(KeyPlayerPrefsRecords))
        {
            PlayerPrefs.SetInt(KeyPlayerPrefsRecords, 0);
        }
                
        PoolEnemy = new PoolGameObjects<Enemy>(EnemyPrefab, SizePoolEnemy);
        PoolEnemy.AftoIncrease = AftoIncreasePool;

        StartGame();

        InitializationGameManeger(); 
    }


    private void InitializationGameManeger()
    {
        CoolDownBoostsActive = new bool[ButtonsForGame.Length];

        for(int i = 0; i < ButtonsForGame.Length; i ++)
        {
            CoolDownBoostsActive[i] = true;
        }

        ButtonsForGame[0].onClick.AddListener(EndGameButtonClikc);
        ButtonsForGame[1].onClick.AddListener(SpawnFreezeButtonClikc);
        ButtonsForGame[2].onClick.AddListener(KILLTHEMButtonClikc);
        ButtonsForGame[3].onClick.AddListener(GunOverloadButtonClick);
        ButtonsForGame[4].onClick.AddListener(SlowdownEnemyButtonClick);

        ButtonGpToMainMenu.onClick.AddListener(GoToMainMenuClick);
        ButtonRestart.onClick.AddListener(RestartClick);

        MusicPlayer.volume = PlayerPrefs.GetFloat(KeyMusicSoundValue);
    }
    // Update is called once per frame
    void Update()
    {
        if(ActiveSpawn && !SpawnFreeze && ActiveGame)
        SpawnEnemy();
        
    }

    private void SpawnEnemy()
    {
            Enemy NewEnemy;
            Vector3 PositionForSpawn;

            if (PoolEnemy.GetFreeObject(out NewEnemy) && SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)].SpawnPointEnemy(out PositionForSpawn))
            {
                StartCoroutine(CoolDownSpawn());

                NewEnemy.SetPositionEnemy(PositionForSpawn);
                NewEnemy.SetSetingsEnemy((int)Math.Floor(UnityEngine.Random.Range(0, DifficultyWave)) , DifficultyStartGame + TimeInGame * DifficultyUp);
                //NewTest.SetSetingsEnemy(0 , DifficultyStartGame + TimeInGame * DifficultyUp);
                NewEnemy.gameObject.SetActive(true);

                Debug.Log(DifficultyStartGame + TimeInGame * DifficultyUp);
            }

    }

    private void StartGame()
    {
        TimeInGame = 0;
        hpWals.NewGame();
        ActiveTimer = true;
        ActiveSpawn = true;
        ActiveGame = true;
        StopCoroutine(CoolDownSpawn());
        StartCoroutine(TimerGame());

        SwitchCanvas();
    }
    
    private void EndGame()
    {
        SetRecordsTexts(SaveRecord());
        ActiveTimer = false;
        ActiveGame = false;

        SwitchCanvas();
    }

    private void SwitchCanvas()
    {
        if(ActiveGame)
        {
            CanvasMenu.SetActive(false);
            CanvasGame.SetActive(true);
        }
        else
        {
            CanvasMenu.SetActive(true);
            CanvasGame.SetActive(false);
        }
    }


    private int SaveRecord()
    {
        int OldRecord = PlayerPrefs.GetInt(KeyPlayerPrefsRecords);
        if (OldRecord < TimeInGame)
        {
            PlayerPrefs.SetInt(KeyPlayerPrefsRecords, TimeInGame);
            return TimeInGame;
        }
        return OldRecord;
    }
    private void SetRecordsTexts(int OldRecord)
    {
        ThisRecord.text = OldRecord.ToString();
        NewRecord.text = TimeInGame.ToString();
    }
        
        
    // Методы для игровых кнопок ------

    private void EndGameButtonClikc()
    {
        EndGame();
    }
    
    private void SpawnFreezeButtonClikc()
    {
        if (!CoolDownBoostsActive[1]) return;

        StartCoroutine(BoostSpawnFreeze());
        StartCoroutine(CoolDownBoost(1 , CoolDownSpawnBoost));
    }
    private void KILLTHEMButtonClikc()
    {
        if (!CoolDownBoostsActive[2]) return;
        StartCoroutine(BoostKILLTHEM());
        StartCoroutine(CoolDownBoost(2, CoolDownSpawnBoost));
    }
    private void GunOverloadButtonClick()
    {
        if (!CoolDownBoostsActive[3]) return;
        playerController.ActiveGunOverload();
        StartCoroutine(CoolDownBoost(3, CoolDownSpawnBoost));
    }
    private void SlowdownEnemyButtonClick()
    {
        if (!CoolDownBoostsActive[4]) return;
        StartCoroutine(BoostSlowdownEnemy());
        StartCoroutine(CoolDownBoost(4, CoolDownSpawnBoost));
        
    }



    /*
     *  Методы для кнопок игрового меню
     */

    private void GoToMainMenuClick()
    {
        if (ActiveGame) return;
        SceneManager.LoadScene(IndexSceneManeMenu);

    }
    
    private void RestartClick()
    {
        if (ActiveGame) return;
        PoolEnemy.DisableAllObjectInPool();
        StartGame();
    }


    /*
     *  Методы для кнопок игрового меню
     */


    public void WallsDead()
    {
        EndGame();
    }

    /*
     *  Корутины для бустов ----------------------------------------------------------
     */



    IEnumerator BoostSpawnFreeze()
    {
        SpawnFreeze = true;
        yield return new WaitForSeconds(TimeSpawnFreeze);
        SpawnFreeze = false;
    }
    
    IEnumerator BoostKILLTHEM()
    {
        TriggerZoneDead.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        TriggerZoneDead.SetActive(false);
    }

    IEnumerator BoostSlowdownEnemy()
    {
        TriggerZoneSlowdown.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        TriggerZoneSlowdown.SetActive(false);
    }

    /*
     *  Корутины для Отката бустов ----------------------------------------------------------
     */
    IEnumerator CoolDownBoost( int IndexBoost, float TimeCoolDown)
    {
        CoolDownBoostsActive[IndexBoost] = false;
        yield return new WaitForSeconds(TimeCoolDown);
        CoolDownBoostsActive[IndexBoost] = true;
    }

    /*
     *  Корутины для GameManeger ----------------------------------------------------------
     */
    IEnumerator CoolDownSpawn()
    {
        Debug.Log(ActiveSpawn);
        ActiveSpawn = false;
        yield return new WaitForSeconds(2 / (DifficultyStartGame + (TimeInGame * DifficultyUp)));
        ActiveSpawn = true;
    }

    IEnumerator TimerGame()
    {
        while(ActiveTimer)
        {
            
            TimeInGame++;
            yield return new WaitForSeconds(1);
            if(TimeInGame % 2 == 0) DifficultyWave = 2;
            ThisRecordGame.text = TimeInGame.ToString();
        }
    }
}
