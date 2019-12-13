using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    public int valueCoin;
    private float speedRotation = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speedRotation * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PanelGamePlayManager.instance.AddCoin(valueCoin);
            Debug.Log("coin kena player : "+valueCoin);
            Destroy(gameObject);
        }
        
        // Debug.Log("coin kena player : "+valueCoin);
    }
}
