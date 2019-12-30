using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dataMusic
{
    public string nameMusic;
    public AudioClip audioClip;
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public List<dataMusic> dataMusics;

    AudioSource audioSource;
    AudioSourceEffek audioSourceEffek;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(string nameAudio)
    {
        foreach (var item in dataMusics)
        {
            if (item.nameMusic.Equals(nameAudio))
            {
                audioSource.clip = item.audioClip;
                audioSource.Play();
                audioSource.loop = true;
                break;
            }
        }
    }

    public AudioClip GetAudioClip(string nameAudio)
    {
        AudioClip resAudio = null;
        foreach (var item in dataMusics)
        {
            if (item.nameMusic.Equals(nameAudio))
            {
                resAudio = item.audioClip;
                break;
            }
        }
        return resAudio;
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

}
