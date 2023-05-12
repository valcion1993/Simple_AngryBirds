using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	private AudioSource[] fxAudioPool;

	void Awake()
	{
		instance = this;

		fxAudioPool = GetComponentsInChildren<AudioSource>();
	}

	public void PlayFx(AudioClip clip)
	{
		for (int i = 0; i < fxAudioPool.Length; i++)
		{
			if (!fxAudioPool[i].isPlaying)
			{
				fxAudioPool[i].clip = clip;
				fxAudioPool[i].Play();

				break;
			}
		}
	}
}
