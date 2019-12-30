using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PanelDialogWindow : MonoBehaviour
{
    public static PanelDialogWindow instance;
    public Text txtTitleDialog;
    public Text txtContentDialog;

    public Button btnOk;
    public GameObject panelDialog;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        panelDialog.SetActive(false);
        foreach (var btn in GetComponentsInChildren<Button>(true))
        {
            btn.onClick.AddListener(() => AudioSourceEffek.instance.ButtonAudio());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDialog(string title, string content)
    {
        panelDialog.SetActive(true);
        txtTitleDialog.text = title;
        txtContentDialog.text = content;
        btnOk.onClick.AddListener(() => panelDialog.SetActive(false));
    }

    public void ShowDialog(string title, string content, UnityAction okAction)
    {
        panelDialog.SetActive(true);
        txtTitleDialog.text = title;
        txtContentDialog.text = content;
        btnOk.onClick.AddListener(() => okAction());
    }
}
