using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelWorldSelection : MonoBehaviour
{
    public ScrollRect scrollViewWorld;
    public Button btnBack;
    public Button btnNext;
    // Start is called before the first frame update
    void Start()
    {
        btnBack.onClick.AddListener(()=> MainMenuManager.instance.ShowPanelCarSelection());
        btnNext.onClick.AddListener(()=> MainMenuManager.instance.ShowPanelSelectMode());

        setUpBtnContent();
    }

    void setUpBtnContent()
    {
        var content = scrollViewWorld.content;
        for(var i=0; i<content.childCount; i++)
        {
            var itemContent = content.GetChild(i).gameObject.AddComponent<Button>();
        }
    }
}
