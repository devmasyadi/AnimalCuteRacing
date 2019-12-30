using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PanelCarSelection : MonoBehaviour
{
    public static PanelCarSelection instance;
    public Text txtCoin;
    public ScrollRect scrollViewCarSelection;
    public ScrollRect scrollViewUpgradeCar;
    public Button btncredits;
    public Button btnTutorial;
    public Button btnBack;
    public Button btnNext;
    public Button btnUnlock;
    public GameObject panelCredits;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        IsShowScrollViewCarSelection(true);
        btnNext.onClick.AddListener(() => NextButton());
        btnBack.onClick.AddListener(() => BackButton());
        btncredits.onClick.AddListener(delegate
        {
            panelCredits.SetActive(true);
        });
        btnTutorial.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("Tutorial");
        });
        setUpButtonCars();
        panelCredits.SetActive(false);
        if (!PlayerPrefs.HasKey("Coin"))
            PlayerPrefs.SetInt("Coin", 0);

        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("Coin", 99999);
        SetCoin(PlayerPrefs.GetInt("Coin"));

        SetLockUnlocokCar();

        foreach (var btn in GetComponentsInChildren<Button>(true))
        {
            btn.onClick.AddListener(() => AudioSourceEffek.instance.ButtonAudio());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {

    }

    void SetLockUnlocokCar()
    {
        var content = scrollViewCarSelection.content;
        var index = 0;
        foreach (var item in content.GetComponentsInChildren<ItemLock>())
        {
            var isLock = CarsSelection.instance.GetListLockCar().Contains(item.gameObject.name) ? true : false;
            item.panelLock.SetActive(isLock);
            item.isLock = isLock;
            item.price = CarsSelection.instance.dataCarSelections[index].price;
            item.txtLevel.text = item.price.ToString();
            index++;
            // Debug.Log(item.gameObject.name+" : "+isLock);
        }
    }

    void IsShowScrollViewCarSelection(bool isShowCarSelect)
    {
        scrollViewCarSelection.gameObject.SetActive(isShowCarSelect);
        scrollViewUpgradeCar.gameObject.SetActive(!isShowCarSelect);
        if (isShowCarSelect)
        {
            btnTutorial.gameObject.SetActive(true);
            btnBack.gameObject.SetActive(false);
            //  btnBack.transform.GetChild(0).GetComponent<Text>().text = "Tutorial";
        }
        else
        {
            btnTutorial.gameObject.SetActive(false);
            btnBack.gameObject.SetActive(true);
            //  btnBack.transform.GetChild(0).GetComponent<Text>().text = "Back";
        }
    }

    public void IsShowBtnUnlock(bool isShowBtnUnlock)
    {
        btnUnlock.gameObject.SetActive(isShowBtnUnlock);
        btnNext.gameObject.SetActive(!isShowBtnUnlock);
    }

    void SetCoin(int coin)
    {
        txtCoin.text = coin.ToString();
    }

    bool BuyCar(int price, GameObject objLock, string nameCar)
    {
        var currCoins = PlayerPrefs.GetInt("Coin");
        if (currCoins >= price)
        {
            currCoins -= price;
            PlayerPrefs.SetInt("Coin", currCoins);
            PanelDialogWindow.instance.ShowDialog("Congrulation", "You have successfully purchased an item");
            SetCoin(currCoins);
            objLock.SetActive(false);
            CarsSelection.instance.UnlockCar(nameCar);
            IsShowBtnUnlock(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    void NextButton()
    {
        if (scrollViewCarSelection.gameObject.activeInHierarchy)
        {
            IsShowScrollViewCarSelection(false);
        }
        else
        {
            MainMenuManager.instance.ShowPanelWorldSelection();
        }
    }

    void BackButton()
    {
        if (scrollViewCarSelection.gameObject.activeInHierarchy)
        {
            Debug.Log("Tutorial");
        }
        else
        {
            IsShowScrollViewCarSelection(true);
        }
    }

    void setUpButtonCars()
    {
        var content = scrollViewCarSelection.content.gameObject;
        for (var i = 0; i < content.transform.childCount; i++)
        {
            var item = content.transform.GetChild(i).gameObject.AddComponent<Button>();
            item.onClick.AddListener(delegate ()
            {
                // Debug.Log(item.gameObject.name);
                var itemLock = item.GetComponent<ItemLock>();
                IsShowBtnUnlock(itemLock.isLock);
                if (itemLock.isLock)
                {
                    btnUnlock.onClick.RemoveAllListeners();
                    if (PlayerPrefs.GetInt("Coin") >= itemLock.price)
                    {
                        btnUnlock.onClick.AddListener(delegate
                        {
                            PanelDialogWindow.instance.ShowDialog("Confirm", "Do you sure want to buy this item wiht " + itemLock.price + " coins ?", delegate
                            {
                                var isBuy = BuyCar(itemLock.price, itemLock.panelLock, item.gameObject.name);
                                itemLock.isLock = false;
                            });
                        });
                    }
                    else
                    {
                        btnUnlock.onClick.AddListener(delegate
                       {
                           PanelDialogWindow.instance.ShowDialog("Confirm", "Sorry you don't have sufficient coins!");
                       });

                    }
                }
                CarsSelection.instance.SpawnModelMainMenu(item.gameObject.name);
            });
        }
    }
}
