using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {

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
