using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameOver : MonoBehaviour
{
    public Button btnBackMainMenu;
    public Button btnTryAgain;
    // Start is called before the first frame update
    void Start()
    {
        btnBackMainMenu.onClick.AddListener(()=>GamePlayManager.instance.BackMainMenu());
        btnTryAgain.onClick.AddListener(()=>GamePlayManager.instance.RestartGamePlay());
    }

  
}
