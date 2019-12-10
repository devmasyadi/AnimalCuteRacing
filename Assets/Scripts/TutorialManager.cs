using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.Equals("BackWard"))
        {
            Debug.Log("kena BackWard");
        }

        Debug.Log("Kena");
    }
}
