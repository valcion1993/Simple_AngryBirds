using UnityEngine;
using System.Collections;
using Assets.Scripts;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class BirdBase : MonoBehaviour
{
	public BirdState State { get; private set; }

	[Header("Impact setting")]
	[SerializeField] float damageMultiplier = 10;

	[Header("Sounds")]
	[SerializeField] private AudioClip shootSound;

	//References
	private TrailRenderer trail;
	private Rigidbody2D rb;
	private CircleCollider2D circleCol2D;

	private bool usedPowerUp;
	private float damage;

	void Start()
	{
		//Get refs
		GetReferences();

		//Set initial settings
		SetSettings();

		//Comprobate destroy
		StartCoroutine(ComprobateToDestroy());
	}

	void GetReferences()
	{
		trail = GetComponent<TrailRenderer>();
		rb = GetComponent<Rigidbody2D>();
		circleCol2D = GetComponent<CircleCollider2D>();
	}

	void SetSettings()
	{
		trail.sortingLayerName = "Foreground";

		circleCol2D.radius = Constants.Bird_Collider_Radius_Big;

		//initial state
		State = BirdState.BeforeThrown;
	}

	//Shot Bird
	public void ShootBird(Vector2 shootDir, float force, float distance)
	{
		rb.velocity = distance * force * new Vector2(shootDir.x, shootDir.y);

		AudioManager.instance.PlayFx(shootSound);

		trail.enabled = true;
		rb.isKinematic = false;

		circleCol2D.radius = Constants.BirdColliderRadiusNormal;
		State = BirdState.Thrown;
	}
	public void TryPowerUp()
	{
		if (!usedPowerUp)
		{
			PowerUp();
		}
	}

	//Protecteds
	protected void DissableBird()
	{
		//Dissable bird
		GetComponent<SpriteRenderer>().enabled = false;
		circleCol2D.enabled = false;
		rb.simulated = false;
	}

	//Virtual Methods
	protected virtual void PowerUp()
	{
		usedPowerUp = true;
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		//Verify damageable
		IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

		if (rb.velocity.magnitude > 3)//Min velocity to play FXs
		{
			ParticlesManager.instance.PlayHitParticles(transform.position);
		}

		//Apply damage
		if (damageable != null)
		{
			damage = rb.velocity.magnitude * damageMultiplier;

			damageable.Damage(damage);

			FloatingTextManager.instance.ShowFloatingText(Mathf.Round(damage).ToString(), transform.position);
			PointsManager.instance.AddPoints(Mathf.FloorToInt(damage));
		}
	}

	//IEnums
	IEnumerator ComprobateToDestroy()
	{
		yield return new WaitForSeconds(0.1f);

		if (State == BirdState.Thrown && rb.velocity.sqrMagnitude <= Constants.Min_Velocity)
			Destroy(gameObject, 2);
		else
			StartCoroutine(ComprobateToDestroy());
	}
}
