using UnityEngine;

public class MultiBird : Bird
{
	[Header("Multi shoots")]
	[SerializeField] private GameObject shooPrefab;
	[SerializeField] private Transform[] spawnDirs;
	[SerializeField] private float shootForce;
	protected override void PowerUp()
	{
		base.PowerUp();

		//Dissable bird
		DissableBird();

		ParticlesManager.instance.PlayHitParticles(transform.position);

		for (int i = 0; i < spawnDirs.Length; i++)
		{
			GameObject newBird = Instantiate(shooPrefab, spawnDirs[i].transform.position, spawnDirs[i].transform.rotation);

			if (i == 0)//Set new bird to follow
				CameraFollow.instance.SetTarget(newBird.transform);

			newBird.GetComponent<TrailRenderer>().enabled = true;
			newBird.GetComponent<Rigidbody2D>().AddForce(spawnDirs[i].right * shootForce, ForceMode2D.Impulse);
		}
	}
}
