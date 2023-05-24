using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public AudioClip[] clips;
	public AudioSource audioSource;
	private int currentClipIdx = -1;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (!audioSource.isPlaying)
		{
			Debug.Log("new clip loaded");
			audioSource.clip = getRandomClip();
			audioSource.Play();
		}
	}

	private AudioClip getRandomClip()
	{
		int randomClipIdx = Random.Range(0, clips.Length);
		while (randomClipIdx == currentClipIdx)
		{
			randomClipIdx = Random.Range(0, clips.Length);
		}

		currentClipIdx = randomClipIdx;
		return clips[randomClipIdx];
	}

	void Update()
	{

		

	}
}

