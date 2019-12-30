using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGamePlayManager : MonoBehaviour
{
    public static PanelGamePlayManager instance;
    public Transform iconCoin;
    public Text txtLevel;
    public Text txtCoin;
    public Text txtDistance;
    public Button btnResume;
    public Image imgSliderFuel;
    Coroutine corFuelSystem;
    GamePlayManager gamePlayManager;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gamePlayManager = FindObjectOfType<GamePlayManager>();
        SetUpButton();
        corFuelSystem = StartCoroutine(FuelSystem(CarController.instance.fuel));
        SetCoin(PlayerPrefs.GetInt("Coin"));
    }

    void SetUpButton()
    {
        btnResume.onClick.AddListener(() => GamePlayManager.instance.ShowPanelResume());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FuelSystem(float fuel)
    {
        var timeLeft = fuel;
        while (timeLeft > 0 && gamePlayManager.state == GamePlayManager.State.play)
        {
            timeLeft -= Time.deltaTime;
            imgSliderFuel.fillAmount = timeLeft / fuel;
            if (timeLeft <= 0)
            {
                GamePlayManager.instance.GameOVer();
                Debug.Log("mati");
            }
            yield return null;
        }
    }

    public void SetTxtLevel(int level)
    {
        txtLevel.text = "Level "+level;
    }

    public void SetTextDistance(string distance)
    {
        txtDistance.text = distance;
    }
    
    public void ResetFuelSystem(float fuel)
    {
        StopCoroutine(corFuelSystem);
        corFuelSystem = StartCoroutine(FuelSystem(fuel));
    }

    void SetCoin(int coin)
    {
        txtCoin.text = coin.ToString();
    }
    public void AddCoin(int coin)
    {
        var currCoin = PlayerPrefs.GetInt("Coin");
        currCoin += coin;
        PlayerPrefs.SetInt("Coin", currCoin);
        SetCoin(currCoin);
    }






}
