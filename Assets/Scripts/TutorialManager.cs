using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject panelResume;
    public static TutorialManager instance;
    public Transform parentPlayer;
    GameObject player;

    private void Awake() {
       if (CarsSelection.instance != null)
            CarsSelection.instance.BaseSpawnModelById(PlayerPrefs.GetString("Player"), true, parentPlayer, -12.94f);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        player.AddComponent<TriggersTutorial>();
        panelResume.SetActive(false);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.Equals("BackWard"))
        {
            Debug.Log("kena BackWard");
        }
    }
}
