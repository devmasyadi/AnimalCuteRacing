using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    public GameObject panelCarSelection;
    public GameObject panelWorldSelection;
    public GameObject panelLevelSelection;
    public GameObject panelSelectMode;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        ShowPanelCarSelection();

        if (!PlayerPrefs.HasKey("Player"))
            PlayerPrefs.SetString("Player", CarsSelection.instance.dataCarSelections[0].id);

        CarsSelection.instance.FirstSpawnModelById(PlayerPrefs.GetString("Player"));
        MusicManager.instance.PlayAudio("MusicMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetBasePanel(bool panelCarSelection, bool panelWorldSelection, bool panelLevelSelection, bool panelSelectMode)
    {
        this.panelCarSelection.SetActive(panelCarSelection);
        this.panelWorldSelection.SetActive(panelWorldSelection);
        this.panelLevelSelection.SetActive(panelLevelSelection);
        this.panelSelectMode.SetActive(panelSelectMode);
    }

    public void ShowPanelCarSelection()
    {
        SetBasePanel(true, false, false, false);
    }

    public void ShowPanelWorldSelection()
    {
        SetBasePanel(false, true, false, false);
    }

    public void ShowPanelLevelSelection()
    {
        SetBasePanel(false, false, true, false);
    }

    public void ShowPanelSelectMode()
    {
        SetBasePanel(false, false, false, true);
    }
}
