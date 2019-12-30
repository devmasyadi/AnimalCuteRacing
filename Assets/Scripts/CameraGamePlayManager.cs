using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGamePlayManager : MonoBehaviour
{
    public static CameraGamePlayManager instance;
    public GameObject cameraGameOver;
    public GameObject driver;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cameraGameOver.gameObject.SetActive(false);
        // driver = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().driverParent.gameObject;
        // if(driver!=null)
        // {
        //     Instantiate(cameraGameOver, driver.transform);
        // }
    }

    // Update is called once per frame
    void Update()
    {
    //    cameraGameOver.transform.position = driver.transform.position;
    }
}
