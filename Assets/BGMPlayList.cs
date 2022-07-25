using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayList : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;
    private int currentClipIdx = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private AudioClip getRandomClip()
    {
        int randomClipIdx = Random.Range(0, clips.Length);
        while(randomClipIdx == currentClipIdx)
        {
            randomClipIdx = Random.Range(0, clips.Length);
        }

        currentClipIdx = randomClipIdx;
        return clips[randomClipIdx];
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("checking for clip playing");

        if (!audioSource.isPlaying)
        {
            Debug.Log("new clip loaded");
            audioSource.clip = getRandomClip();
            audioSource.Play();
        }
        
    }
}
