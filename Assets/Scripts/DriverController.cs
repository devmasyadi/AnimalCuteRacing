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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void SetOnGamePlay(float ngeDrive)
    {
        animator.SetBool(IS_NGE_DRIVE_KEY, true);
        animator.SetFloat(NGE_DRIVE_KEY, ngeDrive);
    }

    
}
