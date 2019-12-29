using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DataWorldSelection
{
    public string nameWorld;
    public List<GameObject> listLevel;
}

public class WorldsSelection : MonoBehaviour
{
    public static WorldsSelection instance;
    public List<DataWorldSelection> worldSelections;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitFirstLockWorld();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitFirstLockWorld()
    {
        if(!PlayerPrefs.HasKey("hasLockWorld"))
        {
            var listWorld = new List<string>();
            foreach(var item in worldSelections)
            {
                listWorld.Add(item.nameWorld);
            }
            PlayerPrefsX.SetStringArray("lockWorld", listWorld.ToArray());
            UnlockWorld(worldSelections[0].nameWorld);
            PlayerPrefs.SetInt("hasLockWorld", 1);
        }
    }

    public void UnlockWorld(string nameWorld)
    {
        var listWorld = PlayerPrefsX.GetStringArray("lockWorld").ToList();
        listWorld.Remove(nameWorld);
        PlayerPrefsX.SetStringArray("lockWorld", listWorld.ToArray());
    }

    public List<string> GetListLockWorld()
    {
        return PlayerPrefsX.GetStringArray("lockWorld").ToList();
    }

    public void SpawnLevelByIndex(string nameWorld, int indexLevel, Transform parentLevel)
    {
        foreach (var item in worldSelections)
        {
            if (item.nameWorld.Equals(nameWorld))
            {
                if(parentLevel.childCount>0)
                    Destroy(parentLevel.GetChild(0).gameObject);
                Instantiate(item.listLevel[indexLevel], parentLevel);
            }
            break;
        }
    }

    public void SpawnLevelByName(string nameLevel, Transform parentLevel)
    {
        foreach (var itemWorld in worldSelections)
        {
            foreach (var itemLevel in itemWorld.listLevel)
            {
                if (itemLevel.gameObject.name.Equals(nameLevel))
                {
                    Instantiate(itemLevel, parentLevel);
                    break;
                }
            }
        }
    }
}
