using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    public static DriverController instance;

    string IS_NGE_DRIVE_KEY = "isNgeDrive";
    string BENSIN_HABIS_KEY = "bensinHabis";
    string MENANG_KEY = "menang";
    string NGE_DRIVE_KEY = "ngeDrive";
    bool isDead;
    Animator animator;


    public Collider mainCollider;
    Collider[] allCollider;
    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        isDead = false;
        allCollider = GetComponentsInChildren<Collider>(true);
        animator = GetComponent<Animator>();
        
        DoRagdoll(false);

    }

    void Update()
    {
        if (!isDead)
        {
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }

    }

    public void SetOnGamePlay(float ngeDrive)
    {
        animator.SetBool(IS_NGE_DRIVE_KEY, true);
        animator.SetFloat(NGE_DRIVE_KEY, ngeDrive);
    }

    public void SetAnimLoss()
    {
        animator.SetTrigger(BENSIN_HABIS_KEY);
    }

    public void SetAnimWin()
    {
        animator.SetTrigger(MENANG_KEY);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var item in allCollider)
            item.enabled = isRagdoll;
        mainCollider.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("DeadTrigger"))
        {
            // Time.timeScale = 0.1f;
            Debug.Log("mati");
            isDead= true;
            GamePlayManager.instance.DeadByTrigger();
            // transform.parent.DetachChildren();
            // CameraGamePlayManager.instance.cameraGameOver.gameObject.SetActive(true);
            // CameraGamePlayManager.instance.cameraGameOver.transform.position = transform.position;
            // DoRagdoll(true);
        }
        if(collision.gameObject.tag.Equals(""))
        {

        }
        // Debug.Log("kena : "+collision.gameObject.tag);
    }


}
