using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public float offsetXDist = 2;
    public float offsetYDist = 2;
    public float offsetZDist = 2;
    private Vector3 offset;

    private bool isTutorial;
  
    // Start is called before the first frame update
    void Start()
    {
      
        isTutorial = SceneManager.GetActiveScene().name == "Tutorial" ? true : false;
        player = GameObject.FindGameObjectWithTag("Player");
        offsetXDist += player.transform.position.x;
        offsetYDist += player.transform.position.y;
        offsetZDist += player.transform.position.z;
        offset = transform.position - new Vector3(offsetXDist, offsetYDist, offsetZDist);
    }

    // Update is called once per frame
    void Update()
    {
        if(GamePlayManager.instance!=null)
        {
            if(player!=null && GamePlayManager.instance.state == GamePlayManager.State.play || GamePlayManager.instance.state == GamePlayManager.State.prepare)
                transform.position = player.transform.position + offset;
        }
        else if(isTutorial)
            transform.position = player.transform.position + offset;
        
    }


}
