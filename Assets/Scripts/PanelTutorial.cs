using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelTutorial : MonoBehaviour
{
    public static PanelTutorial instance;
    public GameObject dummyCoinTxt;
    public Text txtInfoYellow;
    public Text txtInfo;
    public Button btnResume;
    public Button btnThrottle;
    public Button btnBrake;
    public Button btnSkip;
    public GameObject panahKanan;
    public GameObject panahKiri;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetTutorialHoldThrottle();
        SetUpButton();
    }

    void SetTutorialBase(string txtInfo, bool playAnimThrottle, bool playAnimBrake, bool isShowPanahKanan, bool isShowPanahKiri)
    {
        this.txtInfoYellow.gameObject.SetActive(false);
        this.txtInfo.text = txtInfo;
        btnThrottle.GetComponent<Animator>().enabled = playAnimThrottle;
        btnBrake.GetComponent<Animator>().enabled = playAnimBrake;
        panahKanan.SetActive(isShowPanahKanan);
        panahKiri.SetActive(isShowPanahKiri);
    }

    void SetTutorialBase(string txtInfoYellow, string txtInfo, bool playAnimThrottle, bool playAnimBrake, bool isShowPanahKanan, bool isShowPanahKiri)
    {
        this.txtInfoYellow.gameObject.SetActive(true);
        this.txtInfoYellow.text = txtInfoYellow;
        this.txtInfo.text = txtInfo;
        btnThrottle.GetComponent<Animator>().enabled = playAnimThrottle;
        btnBrake.GetComponent<Animator>().enabled = playAnimBrake;
        panahKanan.SetActive(isShowPanahKanan);
        panahKiri.SetActive(isShowPanahKiri);
    }

    void SetUpButton()
    {
        btnSkip.onClick.AddListener(()=>SceneManager.LoadScene("MainMenu"));
        btnThrottle.onClick.AddListener(() => DisableAnimatorThrottle());
        btnBrake.onClick.AddListener(() => DisableAnimatorBrake());
    }

    void SetTutorialHoldThrottle()
    {
        SetTutorialBase("Hold Throttle Button to move forward", true, false, true, false);
    }

    public void SetTutorialHoldBrake()
    {
        SetTutorialBase("Hold Brake Button to go backward", false, true, false, true);
    }

    public void SetTutorialJump()
    {
        SetTutorialBase("Now go jump over another ramp in front", false, false, true, false);
    }

    public void SetTutorialHoldThrottleInAir()
    {
        SetTutorialBase("In Air Control", "Hold Throttle Button to titl backward", true, false, false, false);
    }

    public void DisableAnimatorThrottle()
    {
        if (btnThrottle.GetComponent<Animator>().enabled)
            btnThrottle.GetComponent<Animator>().enabled = false;
    }

    public void DisableAnimatorBrake()
    {
        if (btnBrake.GetComponent<Animator>().enabled)
            btnBrake.GetComponent<Animator>().enabled = false;
    }




}
