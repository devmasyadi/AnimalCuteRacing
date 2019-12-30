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
    Transform lineFinish;
    public Transform lineStart;
    float currentMeter;
    float distance;

    private void Awake()
    {
        if (CarsSelection.instance != null)
            CarsSelection.instance.BaseSpawnModelById(PlayerPrefs.GetString("Player"), true, parentPlayer, parentPlayer.transform.position.z);

        if (WorldsSelection.instance != null)
        {
            nameWorld = PlayerPrefs.GetString("nameWorld");
            indexLevel = PlayerPrefs.GetInt("indexLevel");
            Debug.Log(nameWorld);
            WorldsSelection.instance.SpawnLevelByIndex(nameWorld, indexLevel, parentLevel);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        player.AddComponent<TriggersGamePlay>();
        lineFinish = null;
        lineStart = GameObject.FindGameObjectWithTag("LineStart").transform;
        StartCoroutine(GetLineFinish());
        StartCoroutine(GetDistance());
        StartCoroutine(StartGame());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetLineFinish()
    {
        while (lineFinish == null)
        {
            lineFinish = GameObject.FindGameObjectWithTag("LineFinish").transform;
            yield return null;
        }
    }

    IEnumerator GetDistance()
    {
        while (state == State.play && lineFinish != null)
        {
            distance = (lineFinish.transform.position - lineStart.transform.position).magnitude;
            currentMeter = distance - (lineFinish.transform.position - player.transform.position).magnitude;
            // Debug.Log("distance : "+currentMeter.ToString("F1"));
            PanelGamePlayManager.instance.SetTextDistance(Mathf.Round(currentMeter) + " m / " + Mathf.Round(distance) + " m");
            yield return null;
        }
        if (state == State.finish)
        {
            PanelGamePlayManager.instance.SetTextDistance(Mathf.Round(distance) + " m / " + Mathf.Round(distance) + " m");
            Debug.Log("finish coy");
            yield break;
        }
    }

    void SetUpBasePanel(bool panelResume, bool panelGameOver, bool panelGameFinish)
    {
        this.panelResume.SetActive(panelResume);
        this.panelGameOver.SetActive(panelGameOver);
        this.panelGameFinish.SetActive(panelGameFinish);
    }

    IEnumerator StartGame()
    {
        MusicManager.instance.StopAudio();
        var carController = FindObjectOfType<CarController>();
        yield return new WaitUntil(() => carController != null && carController.isGrounded);
         StartCoroutine(PanelTextInfoGamePlay.instance.ShowPanelTextInfo("GO ... !!!!"));
        yield return new WaitForSeconds(0.8f);
        yield return StartCoroutine(AudioSourceEffek.instance.AudioStartGame());
        Debug.Log(carController);
        PlayGame();
    }

    public void PlayGame()
    {
        state = State.play;
        SetUpBasePanel(false, false, false);
        if (MusicManager.instance != null)
            MusicManager.instance.PlayAudio("MusicWorld_1");
        PanelGamePlayManager.instance.SetTxtLevel(indexLevel + 1);
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

    public void NextLevel()
    {
        PlayerPrefs.SetInt("indexLevel", PlayerPrefs.GetInt("indexLevel") + 1);
        if (indexLevel > PanelLevelSelection.instance.GetListLockLevel(nameWorld).Count - 1)
        {

        }
        else
        {

        }
        PanelLevelSelection.instance.UnlockLevel(nameWorld, indexLevel + 1);
        RestartGamePlay();
    }

    public void RestartGamePlay()
    {
        MusicManager.instance.StopAudio();
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
        SetUpBasePanel(false, false, true);
        Debug.Log("Finish");
    }
}
