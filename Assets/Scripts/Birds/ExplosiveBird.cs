using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBird : BirdBase
{
	[Header("Explosive settings")]
	[SerializeField] private float radius;
	[SerializeField] private float baseDamage;
	[SerializeField] [Range(2, 8)] private float damageFalloff = 5f;
	[SerializeField] [Range(1, 20)] private float explosionForce;

	[Header("Explosive audio clip")]
	[SerializeField] private AudioClip explosiveClip;

	Collider2D[] cols;
	IDamageable damageable;
	float targetDistance;
	float damageDeal;
	float finalForce;
	protected override void PowerUp()
	{
		base.PowerUp();

		//Dissable bird
		DissableBird();

		//Spawn particles
		ParticlesManager.instance.PlayExplosiveParticles(transform.position);
		AudioManager.instance.PlayFx(explosiveClip);

		//Get all cols near
		cols = Physics2D.OverlapCircleAll(transform.position, radius);

		//Search and apply damage
		for (int i = 0; i < cols.Length; i++)
		{
			damageable = cols[i].gameObject.GetComponent<IDamageable>();

			if (damageable != null)
			{
				targetDistance = Vector2.Distance(transform.position, cols[i].transform.position);

				damageDeal = baseDamage - (damageFalloff * targetDistance);

				if (cols[i].GetComponent<Rigidbody2D>())//Add explosive force
				{
					Vector2 direction = cols[i].transform.position - transform.position;

					finalForce = explosionForce * (1.0f - (targetDistance / radius));

					cols[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * finalForce, ForceMode2D.Impulse);
				}

				damageable.Damage(damageDeal);

				FloatingTextManager.instance.ShowFloatingText(Mathf.Round(damageDeal).ToString(), cols[i].transform.position);
				PointsManager.instance.AddPoints(Mathf.FloorToInt(damageDeal));
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
