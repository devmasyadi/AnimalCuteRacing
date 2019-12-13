using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelWorldSelection : MonoBehaviour
{
    public ScrollRect scrollViewWorld;
    public Button btnBack;
    
    // Start is called before the first frame update
    void Start()
    {
        btnBack.onClick.AddListener(()=> MainMenuManager.instance.ShowPanelCarSelection());
        setUpBtnContent();
    }

    void setUpBtnContent()
    {
        var content = scrollViewWorld.content;
        for(var i=0; i<content.childCount; i++)
        {
            var itemContent = content.GetChild(i).gameObject.AddComponent<Button>();
            itemContent.onClick.AddListener(delegate{
                MainMenuManager.instance.ShowPanelLevelSelection();
                MainMenuManager.instance.panelLevelSelection.GetComponent<PanelLevelSelection>().SetPanelSelection(itemContent.gameObject.name);
            });
        }
    }
}
