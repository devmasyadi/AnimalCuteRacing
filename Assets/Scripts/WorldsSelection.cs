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
    public List<DataWorldSelection> worldSelections;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
