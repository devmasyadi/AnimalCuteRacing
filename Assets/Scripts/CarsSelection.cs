using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DataCarSelection
{
    public string id;
    public int price;
    public GameObject character;
    public GameObject vehicle;
}

public class CarsSelection : MonoBehaviour
{
    public static CarsSelection instance;
    public GameObject spawnCharMenu;
    public GameObject spawnVehicleMenu;
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

        InitFirstLockCar();

    }


    void InitFirstLockCar()
    {

        if (!PlayerPrefs.HasKey("hasLockCar"))
        {
            List<string> listCar = new List<string>();

            foreach (var item in dataCarSelections)
                listCar.Add(item.id);

            PlayerPrefsX.SetStringArray("lockCar", listCar.ToArray());
            PlayerPrefs.SetInt("hasLockCar", 1);
            UnlockCar(dataCarSelections[0].id);
        }
    }

    public void UnlockCar(string id)
    {
        var listCar = PlayerPrefsX.GetStringArray("lockCar").ToList();
        listCar.Remove(id);
        PlayerPrefsX.SetStringArray("lockCar", listCar.ToArray());

    }

    public List<string> GetListLockCar()
    {
        return PlayerPrefsX.GetStringArray("lockCar").ToList();
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
        var content = PanelCarSelection.instance.scrollViewCarSelection.content;
        foreach (var item in content.GetComponentsInChildren<ItemLock>())
        {
            if (item.gameObject.name.Equals(id))
            {
                PanelCarSelection.instance.IsShowBtnUnlock(item.isLock);
                // Debug.Log(item);
            }

        }
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
        {
            foreach (var item in parentVehicle.GetComponentsInChildren<CarController>())
                Destroy(item.gameObject);
        }
        foreach (var item in dataCarSelections)
        {
            if (item.id.Equals(id))
            {
                var vehicle = Instantiate(item.vehicle, parentVehicle);
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
