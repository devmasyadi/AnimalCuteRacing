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
        btnBack.onClick.AddListener(() => MainMenuManager.instance.ShowPanelCarSelection());
        setUpBtnContent();
        setUpLockUnlockWorld();
        foreach(var btn in GetComponentsInChildren<Button>(true))
        {
            btn.onClick.AddListener(()=>AudioSourceEffek.instance.ButtonAudio());
        }
    }

    void setUpLockUnlockWorld()
    {
        var content = scrollViewWorld.content;
        foreach (var item in content.GetComponentsInChildren<ItemLock>())
        {
            var isLock = WorldsSelection.instance.GetListLockWorld().Contains(item.gameObject.name) ? true : false;
            item.panelLock.SetActive(isLock);
            item.isLock = isLock;
        }
    }

    void setUpBtnContent()
    {
        var content = scrollViewWorld.content;
        var listWolrd = new List<string>();

        for (var i = 0; i < content.childCount; i++)
        {
            var itemContent = content.GetChild(i).gameObject.AddComponent<Button>();
            var itemLock = itemContent.GetComponent<ItemLock>();

            listWolrd.Add(itemLock.gameObject.name);

            itemContent.onClick.RemoveAllListeners();
            itemContent.onClick.AddListener(delegate
            {
                // Debug.Log(itemLock.isLock);
                if (itemLock.isLock)
                {
                    var index = WorldsSelection.instance.GetListLockWorld().IndexOf(itemLock.gameObject.name);
                    PanelDialogWindow.instance.ShowDialog("Info", "Please Complete World <b>" + listWolrd[index] + "</b> to open this world");
                }
                else
                {
                    MainMenuManager.instance.ShowPanelLevelSelection();
                    MainMenuManager.instance.panelLevelSelection.GetComponent<PanelLevelSelection>().InitFirstLockLevel();
                    MainMenuManager.instance.panelLevelSelection.GetComponent<PanelLevelSelection>().SetPanelSelection(itemContent.gameObject.name);
                }
            });

        }
    }
}
