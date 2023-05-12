using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
	public static ParticlesManager instance;

	[Header("Hit")]
	[SerializeField] private ParticleSystem[] hitParticles;

	[Header("Explosive")]
	[SerializeField] private ParticleSystem[] explosiveParticles;
	void Awake()
	{
		instance = this;
	}

	public void PlayHitParticles(Vector3 pos)
	{
		for (int i = 0; i < hitParticles.Length; i++)
		{
			if (!hitParticles[i].isPlaying)
			{
				hitParticles[i].transform.position = pos;
				hitParticles[i].Play();

				break;
			}
		}
	}

	public void PlayExplosiveParticles(Vector3 pos)
	{
		for (int i = 0; i < explosiveParticles.Length; i++)
		{
			if (!explosiveParticles[i].isPlaying)
			{
				explosiveParticles[i].transform.position = pos;
				explosiveParticles[i].Play();

				break;
			}
		}
	}
}
