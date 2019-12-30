using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersGamePlay : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SetItemCoins();
        SetItemGasFuels();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (GamePlayManager.instance.state == GamePlayManager.State.play)
        {
            if (collider.tag.Equals("ItemGas"))
            {
                collider.GetComponent<MoveTowardsUI>().Target = PanelGamePlayManager.instance.iconCoin.gameObject;
                collider.GetComponent<AudioSource>().Play();
                PanelGamePlayManager.instance.ResetFuelSystem(CarController.instance.fuel);
            }
            if (collider.tag.Equals("LineFinish"))
            {
                GamePlayManager.instance.Finish();
            }

            if (collider.tag.Equals("ItemCoin"))
            {
                if (collider.gameObject.name.Equals("item_coin_1pts"))
                {
                    collider.GetComponent<MoveTowardsUI>().Target = PanelGamePlayManager.instance.txtCoin.gameObject;
                    collider.GetComponent<AudioSource>().Play();
                    PanelGamePlayManager.instance.AddCoin(1);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // if(collision.gameObject.tag.Equals("DeadTrigger"))
        // {
        //     GamePlayManager.instance.DeadByTrigger();
        // }
        
        // Debug.Log("kena : "+other.gameObject.tag);
    }

    void SetItemGasFuels()
    {
        var itemGasFuels = GameObject.FindGameObjectsWithTag("ItemGas");
        AddMoveTowardsUI(itemGasFuels, true, "Gas");
    }

    void SetItemCoins()
    {
        var itemCoins = GameObject.FindGameObjectsWithTag("ItemCoin");
        AddMoveTowardsUI(itemCoins, true, "Coin");
    }

    void AddMoveTowardsUI(GameObject[] objects, bool isRotate, string nameAudio)
    {
        foreach (var item in objects)
        {
            var moveTowardsUI = item.AddComponent<MoveTowardsUI>();
            if (isRotate)
                item.AddComponent<RotateObj>().speedRotation = 180f;
            if (!string.IsNullOrEmpty(nameAudio))
            {
                var audiosource = item.AddComponent<AudioSource>();
                audiosource.playOnAwake = false;
                audiosource.clip = MusicManager.instance.GetAudioClip(nameAudio);
            }
            moveTowardsUI.Speed = 1;
            moveTowardsUI.StopOnArrival = true;
            moveTowardsUI.MoveInZ = false;
        }
    }
}
