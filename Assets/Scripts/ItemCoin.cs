using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    public int valueCoin;
    public ParticleSystem effect;
    public AudioClip soundCoin;
    public float speedRotation = 80f;
    MoveTowardsUI moveTowardsUI;
    AudioSource audioSource;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        effect.playOnAwake = false;
        moveTowardsUI = gameObject.AddComponent<MoveTowardsUI>();
        moveTowardsUI.Speed = 1;
        moveTowardsUI.StopOnArrival = true;
        moveTowardsUI.MoveInZ = false;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = soundCoin;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speedRotation * Time.deltaTime, Space.World);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            audioSource.Play();
            // gameObject.GetComponent<MeshRenderer>().enabled = false;
            moveTowardsUI.Target = PanelGamePlayManager.instance.txtCoin.gameObject;
            var durationPart = effect.duration + effect.startLifetime;
            effect.Play();
            PanelGamePlayManager.instance.AddCoin(valueCoin);
            Debug.Log("coin kena player : " + valueCoin);
            // Destroy(gameObject, durationPart);
        }
    }
}
