using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;
    public enum State
    {
        play,
        resume,
        gameOver,
        finish,
    }
    public State state;
    public GameObject panelResume;
    public GameObject panelGameOver;
    public GameObject panelGameFinish;
    public Transform parentPlayer;
    public Transform parentLevel;

    GameObject player;
    string nameWorld;
    int indexLevel;


    private void Awake()
    {
        if (CarsSelection.instance != null)
            CarsSelection.instance.BaseSpawnModelById(PlayerPrefs.GetString("Player"), true, parentPlayer, -11.8f);

        nameWorld = PlayerPrefs.GetString("nameWorld");
        indexLevel = PlayerPrefs.GetInt("indexLevel");

        // Debug.Log("nameWOrld : " + nameWorld);
        // Debug.Log("indexLevel : " + indexLevel);

        WorldsSelection.instance.SpawnLevelByIndex(nameWorld, indexLevel, parentLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        player.AddComponent<TriggersGamePlay>();
        PlayGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUpBasePanel(bool panelResume, bool panelGameOver, bool panelGameFinish)
    {
        this.panelResume.SetActive(panelResume);
        this.panelGameOver.SetActive(panelGameOver);
        this.panelGameFinish.SetActive(panelGameFinish);
    }

    public void PlayGame()
    {
        state = State.play;
        SetUpBasePanel(false, false, false);
        MusicManager.instance.PlayAudio("MusicWorld_1");
    }

    public void ShowPanelResume()
    {
        state = State.resume;
        SetUpBasePanel(true, false, false);
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void GameOVer()
    {
        state = State.gameOver;
        DriverController.instance.SetAnimLoss();
        SetUpBasePanel(false, true, false);
    }

    public void DeadByTrigger()
    {
        state = State.gameOver;
        DriverController.instance.DoRagdoll(true);
        SetUpBasePanel(false, true, false);
    }

    public void Finish()
    {
        state = State.finish;
        DriverController.instance.SetAnimWin();
        Debug.Log("Finish");
    }
}
