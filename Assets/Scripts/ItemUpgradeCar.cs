using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataItemUpgrade{
    public enum Type{
        Engine,
        Suspension,
        Tires,
        Fuel
    }
    public Type type;
    public int firstPrice;
    public int addValue;
    public int maxUpgrade;
}

public class ItemUpgradeCar : MonoBehaviour
{ 

    public List<DataItemUpgrade> dataItems;

    // Start is called before the first frame update
    void Start()
    {
        AssignDataToPanel();
    }

    public DataItemUpgrade GetData(string type)
    {
        DataItemUpgrade result = null;
        foreach(var item in dataItems)
        {
            if(type.Equals(item.type.ToString()))
            {
                result = item;
                break;
            }
        }
        return result;
    }

    void AssignDataToPanel()
    {
        if(PanelCarSelection.instance!=null)
        {
            var content = PanelCarSelection.instance.scrollViewUpgradeCar.content;
            foreach(var item in content.GetComponentsInChildren<ItemUpgrade>())
            {
                var dataItemUpgrade = GetData(item.typeUpgrade.ToString());
                item.firstprice = dataItemUpgrade.firstPrice;
                item.valueUpgrade = dataItemUpgrade.addValue;
                item.maxUpgrade = dataItemUpgrade.maxUpgrade;
            }
        }
    }

   
}
