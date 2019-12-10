using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    public GameObject panelCarSelection;
    public GameObject panelWorldSelection;
    public GameObject panelSelectMode;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        ShowPanelCarSelection();
        CarsSelection.instance.FirstSpawnModelById("A_01");
        MusicManager.instance.PlayAudio("MusicMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPanelCarSelection()
    {
        panelCarSelection.SetActive(true);
        panelWorldSelection.SetActive(false);
        panelSelectMode.SetActive(false);
    }

    public void ShowPanelWorldSelection()
    {
        panelCarSelection.SetActive(false);
        panelWorldSelection.SetActive(true);
        panelSelectMode.SetActive(false);
    }

    public void ShowPanelSelectMode()
    {
        panelSelectMode.SetActive(true);
    }
}
