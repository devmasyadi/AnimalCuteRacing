using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    public static DriverController instance;
    string IS_IDLE_KEY = "isIdle";
    string IS_NGE_DRIVE_KEY = "isNgeDrive";
    string BENSIN_HABIS_KEY = "bensinHabis";
    string MENANG_KEY = "menang";
    string NGE_DRIVE_KEY = "ngeDrive";
    Animator animator;
    Rigidbody rb;

    public Collider mainCollider;
    Collider[] allCollider;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        allCollider = GetComponentsInChildren<Collider>(true);
        animator = GetComponent<Animator>();
        // rb = GetComponent<Rigidbody>();
        DoRagdoll(false);
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
        // if (!isRagdoll)
        //     rb.constraints = RigidbodyConstraints.FreezeAll;
        // else
        //     rb.constraints = RigidbodyConstraints.None;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("DeadTrigger"))
        {
            Debug.Log("mati");
            transform.parent.DetachChildren();
            DoRagdoll(true);
        }
        Debug.Log("kena : "+collision.gameObject.tag);
    }


}
