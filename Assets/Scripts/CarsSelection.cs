using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataCarSelection
{
    public string id;
    public GameObject character;
    public GameObject vehicle;
}

public class CarsSelection : MonoBehaviour
{
    public static CarsSelection instance;
    public GameObject spawnCharMenu;
    public GameObject spawnVehicleMenu;
    public GameObject carSelected;
    public List<DataCarSelection> dataCarSelections;


    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Player"))
            PlayerPrefs.SetString("Player", dataCarSelections[0].vehicle.name);

    }

    void HandleNull()
    {
        if (spawnCharMenu == null)
            spawnCharMenu = GameObject.Find("SpawnCharacter");
        if (spawnVehicleMenu == null)
            spawnVehicleMenu = GameObject.Find("SpawnVehicle");
    }

    public void FirstSpawnModelById(string id)
    {
        HandleNull();
        SpawnModelMainMenu(id);
    }

    /* Old
    public void SpawnModelById(string id)
    {
        if (spawnCharMenu.transform.childCount > 0)
            Destroy(spawnCharMenu.transform.GetChild(0).gameObject);
        if (spawnVehicleMenu.transform.childCount > 0)
            Destroy(spawnVehicleMenu.transform.GetChild(0).gameObject);

        foreach (var item in dataCarSelections)
        {
            if (item.id.Equals(id))
            {
                Instantiate(item.character, spawnCharMenu.transform);
                Instantiate(item.vehicle, spawnVehicleMenu.transform);
                break;
            }
        }
    }
     */

    public void SpawnModelMainMenu(string id)
    {
        BaseSpawnModelById(id, false, spawnVehicleMenu.transform, 0);
    }

    public void BaseSpawnModelById(string id, bool isUsInput, Transform parentVehicle, float positionZ)
    {
        if (parentVehicle.childCount > 0)
            Destroy(parentVehicle.GetChild(0).gameObject);
        foreach (var item in dataCarSelections)
        {
            if (item.id.Equals(id))
            {
                var vehicle = Instantiate(item.vehicle, parentVehicle);
                carSelected = vehicle;
                var carController = vehicle.GetComponent<CarController>();
                carController.isUsInput = isUsInput;
                carController.positionZ = positionZ;
                var parentDriver = carController.driverParent.transform;
                if (parentDriver.childCount > 0)
                    Destroy(parentDriver.GetChild(0).gameObject);
                Instantiate(item.character, parentDriver);
                PlayerPrefs.SetString("Player", id);
                break;
            }
        }
    }
}
