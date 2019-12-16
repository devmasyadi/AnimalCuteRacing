using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deklarasi untuk mengambil index scene
using UnityEngine.SceneManagement;

public class MoveStage : MonoBehaviour
{
    public int nextSceneLoad;

    // Start is called before the first frame update
    void Start()
    {
        // Menset nextSceneLoad bernilai lebih 1 dari index pada build project
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        // Fungsi untuk mengecek apakah bertabrakan dengan collider player
        if(other.gameObject.tag == "Player")
        {
            // Jika index level pada project build hanya bernilai 8
            if(SceneManager.GetActiveScene().buildIndex == 8)
            {
                Debug.Log("Silankan Pindah World");
            }

            else
            {
                // Berpindah ke stage selanjutnya
                SceneManager.LoadScene(nextSceneLoad);

                // Mengecek nilai nextSceneLoad dengan PlayerPrefs
                if(nextSceneLoad > PlayerPrefs.GetInt("LevelAt"))
                {
                    // Set LevelAt bernilai sama dengan nextSceneLoad
                    PlayerPrefs.SetInt("LevelAt",nextSceneLoad);
                }
            }
            
        }
        
    }
}
