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
        Time.timeScale = 0f;
        btnBackMainMenu.onClick.AddListener(()=>BackMainMenu());
        btnRestart.onClick.AddListener(()=>Restart());
        btnResume.onClick.AddListener(()=>Resume());
    }

    void BackMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        // StartCoroutine(showModelAgain(0.1f));
    }

    IEnumerator showModelAgain(float time)
    {
        yield return new WaitForSeconds(time);
        CarsSelection.instance.SpawnModelMainMenu("A_01");
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Resume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
