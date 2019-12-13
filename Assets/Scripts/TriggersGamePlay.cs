using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersGamePlay : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (GamePlayManager.instance.state == GamePlayManager.State.play)
        {
            if (collider.tag.Equals("ItemGas"))
            {
                Destroy(collider.gameObject);
                PanelGamePlayManager.instance.ResetFuelSystem(CarController.instance.fuel);
            }
            if(collider.tag.Equals("LineFinish"))
            {
                GamePlayManager.instance.Finish();
            }
            // if(collider.tag.Equals("ItemCoin1"))
            // {
            //     Debug.Log("Kena : "+collider.tag);
            // }
            // if(collider.tag.Equals("ItemCoin5"))
            // {
            //     Debug.Log("Kena : "+collider.tag);
            // }
        }
    }
}
