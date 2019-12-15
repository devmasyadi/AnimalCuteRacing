using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLevelSelection : MonoBehaviour
{
    public static PanelLevelSelection instance;
    public Button btnBack;
    public Text txtNameWorld;
    public GameObject parentContent;
    public GameObject prefabItem;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        btnBack.onClick.AddListener(()=>MainMenuManager.instance.ShowPanelWorldSelection());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPanelSelection(string nameWorld)
    {
        foreach (var itemWorld in WorldsSelection.instance.worldSelections)
        {
            if (itemWorld.nameWorld.Equals(nameWorld))
            {
                txtNameWorld.text = itemWorld.nameWorld;
                if (parentContent.transform.childCount > 0)
                {
                    foreach (var listChildParent in parentContent.GetComponentsInChildren<Image>())
                    {
                        Destroy(listChildParent.gameObject);
                    }
                }
                for (var i = 0; i < itemWorld.listLevel.Count; i++)
                {
                    var index = i+1;
                    var item = Instantiate(prefabItem, parentContent.transform);
                    item.gameObject.name = itemWorld.listLevel[i].gameObject.name;
                    item.transform.GetChild(0).GetComponent<Text>().text = index.ToString();
                    item.AddComponent<Button>().onClick.AddListener(delegate{
                        PlayerPrefs.SetString("nameWorld", nameWorld);
                        PlayerPrefs.SetInt("indexLevel", index-1);
                        MainMenuManager.instance.ShowPanelSelectMode();
                    });
                }
                break;
            }
        }

    }
}
