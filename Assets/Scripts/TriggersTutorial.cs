using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TriggersTutorial : MonoBehaviour
{
    enum State
    {
        start,
        forward,
        backWard,
        jump,
        forwardInAir,
        backWardInAir,
        done
    }

    State state;
    GameObject player;
    Rigidbody _rigidbody;
    PanelTutorial panelTutorial;
    BoxCollider forwardTrigger;

    float dirX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = player.GetComponent<Rigidbody>();
        panelTutorial = FindObjectOfType<PanelTutorial>();
        forwardTrigger = GameObject.FindGameObjectWithTag("ForwardTrigger").GetComponent<BoxCollider>();
        forwardTrigger.enabled = false;

        panelTutorial.btnBrake.onClick.AddListener(() => DisableKinematic());

        SetItemCoins();
        SetItemGas();
    }

    void Update()
    {

        dirX = Input.GetAxis("Horizontal");
        dirX = CrossPlatformInputManager.GetAxis("Horizontal");

        if (state == State.jump || state == State.forwardInAir || state == State.backWardInAir || state == State.done)
            rotasiCar(-dirX);
        // if (state == State.forwardInAir)
        // {

        // }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("BackWardTrigger"))
        {
            state = State.forward;
            collider.enabled = false;
            _rigidbody.isKinematic = true;
            forwardTrigger.enabled = true;
            PanelTutorial.instance.SetTutorialHoldBrake();
            Debug.Log("kena BackWard");
        }
        else if (collider.gameObject.tag.Equals("ForwardTrigger"))
        {
            collider.enabled = false;
            state = State.backWard;
            PanelTutorial.instance.SetTutorialJump();
        }
        else if (collider.gameObject.tag.Equals("ItemGas"))
        {
            collider.GetComponent<MoveTowardsUI>().Target = PanelTutorial.instance.dummyCoinTxt;
            collider.enabled = false;
            Debug.Log("Kena : " + collider.gameObject);
            // Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag.Equals("ItemCoin"))
        {
            collider.GetComponent<MoveTowardsUI>().Target = PanelTutorial.instance.dummyCoinTxt;
            collider.enabled = false;
            Debug.Log("Kena : " + collider.gameObject);
            // Destroy(collider.gameObject);
        }
        else if (collider.gameObject.name.Equals("JumpTrigger"))
        {
            state = State.forwardInAir;
            collider.enabled = false;
            _rigidbody.isKinematic = true;
            PanelTutorial.instance.SetTutorialHoldThrottleInAir();
        }
        Debug.Log("kena aja");
    }

    int countClickStart = 0;
    void StartTutorial()
    {
        if (countClickStart == 0)
        {

        }
        countClickStart++;
    }

    public void DisableKinematic()
    {
        if (_rigidbody.isKinematic)
            _rigidbody.isKinematic = false;
    }

    void rotasiCar(float dirX)
    {
        transform.Rotate(Vector3.right * dirX * 100 * Time.deltaTime);
    }

    void SetItemCoins()
    {
        var itemCoins = GameObject.FindGameObjectsWithTag("ItemCoin");
        AddMoveTowardsUI(itemCoins, true);
    }

    void SetItemGas()
    {
        var itemGass = GameObject.FindGameObjectsWithTag("ItemGas");
        AddMoveTowardsUI(itemGass, false);
    }

    void AddMoveTowardsUI(GameObject[] objects, bool isRotate)
    {
        foreach (var item in objects)
        {
            var moveTowardsUI = item.AddComponent<MoveTowardsUI>();
            if(isRotate)
                item.AddComponent<RotateObj>().speedRotation = 180f;
            moveTowardsUI.Speed = 1;
            moveTowardsUI.StopOnArrival = true;
            moveTowardsUI.MoveInZ = false;
        }
    }
}
