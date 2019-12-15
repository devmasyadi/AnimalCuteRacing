// code ini digunakan untuk menentukkan level
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deklarasi library
using UnityEngine.UI;


public class SelectLevel : MonoBehaviour
{
    // array button untuk setiap level
    public Button[] lvlButton;

    // Start is called before the first frame update
    void Start()
    {
        // Misal set level pertama di index ke 2 sesuai index pada build setting
        // Menggunakan Playerprefs agar nilai tersimpan
        int levelAt = PlayerPrefs.GetInt("Level",2);

        // fungsi menonaktifkan stage yang levelnya lebih besar dari level pemain
        for(int i = 0; i < lvlButton.Length; i++)
        {
            if(i + 2 > levelAt)
            {
                // fungsi menonaktifkan button
                lvlButton[i].interactable = false;
            }
        }
    }

}
