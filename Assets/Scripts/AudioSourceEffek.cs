using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSourceEffek : MonoBehaviour
{
    public static AudioSourceEffek instance;
    AudioSource audioSource;
    private void Awake()
    {
        if (AudioSourceEffek.instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonAudio()
    {
        audioSource.clip = MusicManager.instance.GetAudioClip("Click");
        audioSource.Play();
    }

    public IEnumerator AudioStartGame()
    {
        audioSource.clip = MusicManager.instance.GetAudioClip("CountdownFx");
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length+0.5f);
        audioSource.clip = MusicManager.instance.GetAudioClip("CountdownStartFx");
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }
}
