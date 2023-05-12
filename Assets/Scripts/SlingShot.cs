using UnityEngine;
using Assets.Scripts;
using System;

public class SlingShot : MonoBehaviour
{
	public Transform BirdWaitPosition;

	[HideInInspector]
	public Bird BirdToThrow;

	[HideInInspector]
	public float TimeSinceThrown;

	[HideInInspector]
	public SlingshotState SlingshotState;

	//Serializeds
	[SerializeField] private Transform LeftSlingshotOrigin, RightSlingshotOrigin;
	[SerializeField] private LineRenderer SlingshotLineRenderer1;
	[SerializeField] private LineRenderer SlingshotLineRenderer2;
	[SerializeField] private LineRenderer TrajectoryLineRenderer;
	[SerializeField] private float ThrowSpeed;

	//Private
	private Vector3 slingshotMiddleVector;
	private Camera mainCamera;

	//Public Events
	public event EventHandler BirdThrown;

	#region Unity
	void Start()
	{
		//Get main camera
		mainCamera = Camera.main;

		SetSettings();
	}

	void SetSettings()
	{
		//Set initial state
		SlingshotState = SlingshotState.Idle;

		//Set initial positions to line renderers
		SlingshotLineRenderer1.SetPosition(0, LeftSlingshotOrigin.position);
		SlingshotLineRenderer2.SetPosition(0, RightSlingshotOrigin.position);

		//Set sort layer
		SlingshotLineRenderer1.sortingLayerName = "Foreground";
		SlingshotLineRenderer2.sortingLayerName = "Foreground";
		TrajectoryLineRenderer.sortingLayerName = "Foreground";

		//Calculate slingshot middle position
		slingshotMiddleVector = new Vector3((LeftSlingshotOrigin.position.x + RightSlingshotOrigin.position.x) / 2,
			(LeftSlingshotOrigin.position.y + RightSlingshotOrigin.position.y) / 2, 0);
	}

	void Update()
	{
		SlinhshotUpdate();
	}
	#endregion

	#region Slingshot_Update_States
	void SlinhshotUpdate()
	{
		switch (SlingshotState)
		{
			case SlingshotState.Idle:
				SlingshotIdle();
				break;

			case SlingshotState.UserPulling:
				SlingshotPulling();
				break;

			case SlingshotState.BirdFlying:
				BirdFliying();
				break;
		}
	}

	void SlingshotIdle()
	{
		InitializeBird();
		DisplaySlingshotLineRenderers();
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 location = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			if (BirdToThrow.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(location))
			{
				SlingshotState = SlingshotState.UserPulling;
			}
		}
	}

	void SlingshotPulling()
	{
		DisplaySlingshotLineRenderers();

		if (Input.GetMouseButton(0))
		{
			Vector3 location = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			location.z = 0;
			if (Vector3.Distance(location, slingshotMiddleVector) > 1.5f)
			{
				var maxPosition = (location - slingshotMiddleVector).normalized * 1.5f + slingshotMiddleVector;
				BirdToThrow.transform.position = maxPosition;
			}
			else
			{
				BirdToThrow.transform.position = location;
			}
			float distance = Vector3.Distance(slingshotMiddleVector, BirdToThrow.transform.position);
			ShowShootTrayectory(distance);
		}
		else
		{
			Set_TrajectoryLineRenderesActive(false);
			TimeSinceThrown = Time.time;
			float distance = Vector3.Distance(slingshotMiddleVector, BirdToThrow.transform.position);
			if (distance > 1)
			{
				SetSlingshot_LineRenderersActive(false);
				SlingshotState = SlingshotState.BirdFlying;
				ShootBird(distance);
			}
			else
			{
				BirdToThrow.transform.positionTo(distance / 10,
					BirdWaitPosition.transform.position).
					setOnCompleteHandler((x) =>
					{
						x.complete();
						x.destroy();
						InitializeBird();
					});

			}
		}
	}

	void BirdFliying()
	{
		//Bird power up
		if (Input.GetMouseButtonDown(0))
			BirdToThrow.TryPowerUp();
	}
	#endregion

	#region SlingShot_Methods
	void InitializeBird()
	{
		BirdToThrow.transform.position = BirdWaitPosition.position;
		SlingshotState = SlingshotState.Idle;
		SetSlingshot_LineRenderersActive(true);
	}

	void SetSlingshot_LineRenderersActive(bool active)
	{
		SlingshotLineRenderer1.enabled = active;
		SlingshotLineRenderer2.enabled = active;
	}

	void DisplaySlingshotLineRenderers()
	{
		SlingshotLineRenderer1.SetPosition(1, BirdToThrow.transform.position);
		SlingshotLineRenderer2.SetPosition(1, BirdToThrow.transform.position);
	}
	void ShowShootTrayectory(float distance)
	{
		Set_TrajectoryLineRenderesActive(true);
		Vector3 v2 = slingshotMiddleVector - BirdToThrow.transform.position;
		int segmentCount = 15;

		Vector2[] segments = new Vector2[segmentCount];

		segments[0] = BirdToThrow.transform.position;
		Vector2 segVelocity = new Vector2(v2.x, v2.y) * ThrowSpeed * distance;

		for (int i = 1; i < segmentCount; i++)
		{
			float time2 = i * Time.fixedDeltaTime * 5;
			segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
		}

		TrajectoryLineRenderer.positionCount = segmentCount;

		for (int i = 0; i < segmentCount; i++)
			TrajectoryLineRenderer.SetPosition(i, segments[i]);
	}

	void Set_TrajectoryLineRenderesActive(bool active)
	{
		TrajectoryLineRenderer.enabled = active;
	}
	#endregion

	void ShootBird(float distance)
	{
		Vector3 velocity = slingshotMiddleVector - BirdToThrow.transform.position;

		BirdToThrow.GetComponent<Bird>().ShootBird(velocity, ThrowSpeed, distance);

		if (BirdThrown != null)
			BirdThrown(this, EventArgs.Empty);
	}
}
