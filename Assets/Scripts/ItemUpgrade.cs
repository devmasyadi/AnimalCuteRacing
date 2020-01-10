using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
    public enum TypeUpgrade
    {
        Engine,
        Suspension,
        Tires,
        Fuel
    }

    Button btnItem;

    public TypeUpgrade typeUpgrade;
    public int valueUpgrade;
    public int firstprice;
    public int maxUpgrade;
    public Image imageSlider;
    public Text txtPrice;

    [HideInInspector]
    public string key;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Button>() == null)
            btnItem = gameObject.AddComponent<Button>();
        btnItem.onClick.AddListener(() => upgradeCar(typeUpgrade, valueUpgrade, getPrice()));

    }

    private void OnEnable()
    {
        var carController = FindObjectOfType<CarController>();
        if (carController != null)
        {
            key = carController.gameObject.name+typeUpgrade.ToString()+"index";
            Debug.Log("key : "+key+" ,value : "+ PlayerPrefs.GetFloat(key));
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetFloat(key, 0.1f);
            imageSlider.fillAmount = PlayerPrefs.GetFloat(key);
            setTextPrice();
        }
        else
        {
             Debug.Log("Tidak ada ItemUpgradeCar");
        }
       
    }

    void upgradeCar(TypeUpgrade typeUpgrade, int valueUpgrade, int price)
    {
        var valueSlider = imageSlider.fillAmount;
        //sudah berapa kali upgrade
        var countUpgrade = (valueSlider * 10f) / 100f * maxUpgrade;
        Debug.Log("countUpgrade" + countUpgrade);
        var currCoint = PlayerPrefs.GetInt("Coin");
        if (valueSlider < 1f)
        {
            if (currCoint >= price)
            {
                switch (typeUpgrade)
                {
                    case TypeUpgrade.Engine:
                        CarController.instance.addValueEngine(valueUpgrade);
                        break;
                    case TypeUpgrade.Fuel:
                        CarController.instance.addValueFuel(valueUpgrade);
                        break;

                }

                currCoint -= price;
                PlayerPrefs.SetInt("Coin", currCoint);
                PanelCarSelection.instance.SetCoin(currCoint);
                float incrementPerUpgrade = 1f / maxUpgrade;

                countUpgrade += incrementPerUpgrade;
                PlayerPrefs.SetFloat(key, countUpgrade);
                imageSlider.fillAmount = PlayerPrefs.GetFloat(key);
                setTextPrice();
                PanelDialogWindow.instance.ShowDialog("Congrulation", "You have successfully upgraded item");
            }
            else
            {
                PanelDialogWindow.instance.ShowDialog("Sorry", "You need " + currCoint + " to upgrade " + typeUpgrade);
            }
        }
        else
        {
            PanelDialogWindow.instance.ShowDialog("Sorry", "You have reached the upgrade limit");
        }

    }

    int getPrice()
    {
        var countUpgrade = ((imageSlider.fillAmount * 10) / 100 * maxUpgrade) * 10;
        return countUpgrade > 0 ? firstprice * (int)countUpgrade : firstprice;
    }

    void setTextPrice()
    {
        txtPrice.text = getPrice().ToString();
    }

}
