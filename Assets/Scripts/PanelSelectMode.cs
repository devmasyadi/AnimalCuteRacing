using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelSelectMode : MonoBehaviour
{
    public Button btnSingleMode;
    public Button btnBack;
    // Start is called before the first frame update
    void Start()
    {
        btnSingleMode.onClick.AddListener(() => SingleMode());
        btnBack.onClick.AddListener(() => MainMenuManager.instance.ShowPanelLevelSelection());
        foreach (var btn in GetComponentsInChildren<Button>(true))
        {
            btn.onClick.AddListener(() => AudioSourceEffek.instance.ButtonAudio());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SingleMode()
    {
        SceneManager.LoadScene("GamePlay");
        // MusicManager.instance.SetAudio("MusicWorld_1");
    }
}
