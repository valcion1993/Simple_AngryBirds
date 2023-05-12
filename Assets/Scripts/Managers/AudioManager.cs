using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	[Header("Audio Components")]
	[SerializeField] private AudioSource musicAudio;
	[SerializeField] private AudioSource[] fxAudioPool;

	void Awake()
	{
		instance = this;
	}

	public void PlayMusic(AudioClip clip)
	{
		musicAudio.clip = clip;
		musicAudio.Play();
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
