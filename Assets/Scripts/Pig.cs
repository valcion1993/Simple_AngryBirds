using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour, IDamageable
{
	public float Health { get; set; } = 150f;

	[Header("Health settings")]
	[SerializeField] private float health = 70;
	[SerializeField] private float ChangeSpriteHealth = 30;
	[SerializeField] private Sprite SpriteShownWhenHurt;

	[Header("Audio")]
	[SerializeField] private AudioClip audioClip;

	void Start()
	{
		Health = health;
	}

	//Comprobate others rigidbodies to apply damage
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.GetComponent<Rigidbody2D>() == null)
			return;

		float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

		Health -= damage;
		if (damage >= 10)
			AudioManager.instance.PlayFx(audioClip);

		if (Health < ChangeSpriteHealth)
			GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;

		if (Health <= 0)
			Destroy(gameObject);
	}

	public void Damage(float damageValue)
	{
		AudioManager.instance.PlayFx(audioClip);

		Health -= damageValue;

		if (Health < ChangeSpriteHealth)
			GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;

		if (Health <= 0)
			Destroy(gameObject);
	}
}
