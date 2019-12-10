using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelCarSelection : MonoBehaviour
{
    public ScrollRect scrollViewCarSelection;
    public ScrollRect scrollViewUpgradeCar;
    public Button btncredits;
    public Button btnTutorial;
    public Button btnBack;
    public Button btnNext;
    public GameObject panelCredits;
    // Start is called before the first frame update
    void Start()
    {
        IsShowScrollViewCarSelection(true);
        btnNext.onClick.AddListener(() => NextButton());
        btnBack.onClick.AddListener(() => BackButton());
        btncredits.onClick.AddListener(() => panelCredits.SetActive(true));
        btnTutorial.onClick.AddListener(()=> SceneManager.LoadScene("Tutorial"));
        setUpButtonCars();
        panelCredits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {

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
                Debug.Log(item.gameObject.name);
                CarsSelection.instance.SpawnModelMainMenu(item.gameObject.name);
            });
        }
    }
}
