using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour, IDamageable
{
	public float Health { get; set; }

	[SerializeField] private float health = 70;

	[Header("Audio")]
	[SerializeField] private AudioClip audioClip;

	void Start()
	{
		Health = health;
	}

	//Interface IDamageable method
	public void Damage(float damageValue)
	{
		Health -= damageValue;
		health = Health;

		if (damageValue >= 10)
			AudioManager.instance.PlayFx(audioClip);

		if (Health <= 0)
			Destroy(gameObject);
	}
}
