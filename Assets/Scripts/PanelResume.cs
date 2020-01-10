using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelResume : MonoBehaviour
{
    public Button btnBackMainMenu;
    public Button btnRestart;
    public Button btnResume;
    // Start is called before the first frame update
    void Start()
    {
        btnBackMainMenu.onClick.AddListener(() => BackMainMenu());
        btnRestart.onClick.AddListener(() => Restart());
        btnResume.onClick.AddListener(() => Resume());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BackMainMenu()
    {
        if(Time.timeScale!=1f)
            Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void Restart()
    {
        if(Time.timeScale!=1f)
            Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Resume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}
