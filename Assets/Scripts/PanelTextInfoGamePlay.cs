using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTextInfoGamePlay : MonoBehaviour
{
    public static PanelTextInfoGamePlay instance;
    public GameObject panel;
    public Text txtInfo;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowPanelTextInfo(string info)
    {
        panel.SetActive(true);
        txtInfo.text = info;
        animator.SetTrigger("infoKeKiri");
        yield return new WaitUntil(()=> animator.GetCurrentAnimatorStateInfo(0).IsName("AnimTextInfoKekiri"));
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length-0.3f);
        panel.SetActive(false);

    }
}
