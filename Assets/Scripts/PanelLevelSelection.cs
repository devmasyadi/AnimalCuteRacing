using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


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
        btnBack.onClick.AddListener(() => MainMenuManager.instance.ShowPanelWorldSelection());

        foreach (var btn in GetComponentsInChildren<Button>(true))
        {
            btn.onClick.AddListener(() => AudioSourceEffek.instance.ButtonAudio());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitFirstLockLevel()
    {
        if (!PlayerPrefs.HasKey("lockLevel"))
        {
            foreach (var item in WorldsSelection.instance.worldSelections)
            {
                List<int> listItem = new List<int>();

                for (var i = 0; i < item.listLevel.Count; i++)
                {
                    listItem.Add(i);
                }

                PlayerPrefsX.SetIntArray("levelLocked" + item.nameWorld, listItem.ToArray());
            }

            UnlockLevel(WorldsSelection.instance.worldSelections[0].nameWorld, 0);
            PlayerPrefs.SetInt("lockLevel", 1);
        }

    }

    public void UnlockLevel(string nameWorld, int indexLevel)
    {
        var tempListLockLevel = PlayerPrefsX.GetIntArray("levelLocked" + nameWorld).ToList();
        tempListLockLevel.Remove(indexLevel);
        PlayerPrefsX.SetIntArray("levelLocked" + nameWorld, tempListLockLevel.ToArray());
    }

    public List<int> GetListLockLevel(string nameWorld)
    {
        return PlayerPrefsX.GetIntArray("levelLocked" + nameWorld).ToList();
    }

    // void setLockUnlockLevelSelection()
    // {
    //     foreach(var item in parentContent.GetComponentsInChildren<ItemLock>())
    //     {
    //         var islocked = GetListLockLevel().Contains(item.gameObject.name);
    //     }
    // }

    public void SetPanelSelection(string nameWorld)
    {
        var listLocked = GetListLockLevel(nameWorld);

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
                    var index = i + 1;
                    var item = Instantiate(prefabItem, parentContent.transform);

                    var islocked = listLocked.Contains(i) ? true : false;

                    var ItemLock = item.GetComponent<ItemLock>();
                    ItemLock.panelLock.SetActive(islocked);
                    ItemLock.txtLevel.text = index.ToString();

                    item.gameObject.name = itemWorld.listLevel[i].gameObject.name;
                    if (!islocked)
                    {
                        item.AddComponent<Button>().onClick.AddListener(delegate
                        {
                            PlayerPrefs.SetString("nameWorld", nameWorld);
                            PlayerPrefs.SetInt("indexLevel", index - 1);
                            MainMenuManager.instance.ShowPanelSelectMode();
                        });
                    }

                }
                break;
            }
        }

    }
}
