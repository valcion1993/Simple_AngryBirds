using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	//Publics
	[SerializeField] private CameraFollow cameraFollow;
	[SerializeField] private SlingShot slingshot;

	public static GameState CurrentGameState = GameState.Start;

	//Privates
	private List<GameObject> Bricks;
	private List<GameObject> Birds = new List<GameObject>();
	private List<GameObject> Pigs;

	int currentBirdIndex;
	Camera mainCamera;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		//Get Refs
		mainCamera = Camera.main;

		//Register action
		slingshot.BirdThrown += Slingshot_BirdThrown;

		//Set Settings
		SetSettings();
	}

	void SetSettings()
	{
		//Get entities
		Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
		Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));

		CurrentGameState = GameState.Start;
		slingshot.enabled = false;
	}

	void OnDestroy()
	{
		slingshot.BirdThrown -= Slingshot_BirdThrown;
	}

	void Update()
	{
		GameStateUpdate();
	}

	public void AddBird(GameObject newBird)
	{
		Birds.Add(newBird);
	}

	public void GameStart()
	{
		AnimateBirdToSlingshot();
	}

	//Update playing game state
	void GameStateUpdate()
	{
		if (CurrentGameState == GameState.Playing)
		{
			if (slingshot.SlingshotState == SlingshotState.BirdFlying && (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
			{
				slingshot.enabled = false;
				AnimateCamera_ToStartPosition();
				CurrentGameState = GameState.BirdMovingToSlingshot;
			}
		}
	}

	#region Slingshot

	//Move bird to slingshot center
	void AnimateBirdToSlingshot()
	{
		CurrentGameState = GameState.BirdMovingToSlingshot;

		Birds[currentBirdIndex].transform.positionTo
			(Vector2.Distance(Birds[currentBirdIndex].transform.position / 10,
			slingshot.BirdWaitPosition.transform.position) / 10, //duration
			slingshot.BirdWaitPosition.transform.position). //final position
				setOnCompleteHandler((x) =>
				{
					x.complete();
					x.destroy();
					CurrentGameState = GameState.Playing;
					slingshot.enabled = true;
					slingshot.BirdToThrow = Birds[currentBirdIndex].GetComponent<BirdBase>();
				});
	}

	//Thrown a bird
	void Slingshot_BirdThrown(object sender, System.EventArgs e)
	{
		cameraFollow.SetTarget(Birds[currentBirdIndex].transform);
		cameraFollow.IsFollowing = true;
	}
	#endregion

	//Move camera to start pos
	void AnimateCamera_ToStartPosition()
	{
		float duration = Vector2.Distance(mainCamera.transform.position, cameraFollow.StartingPosition) / 10f;

		if (duration == 0.0f)
			duration = 0.1f;

		//animate the camera to start
		mainCamera.transform.positionTo
			(duration,
			cameraFollow.StartingPosition). //end position
			setOnCompleteHandler((x) =>
			{
				cameraFollow.IsFollowing = false;

				if (AllEnemyDestroy())//Win Game
				{
					MessageManager.instance.ShowMessage("You win the game!", 5);
					HudManager.instance.GameOverScreen.SetActive(true);
					CurrentGameState = GameState.Won;
				}
				else if (currentBirdIndex == Birds.Count - 1)//Lost game
				{
					MessageManager.instance.ShowMessage("You lost, try again!", 5);
					HudManager.instance.GameOverScreen.SetActive(true);
					CurrentGameState = GameState.Lost;
				}
				else
				{
					slingshot.SlingshotState = SlingshotState.Idle;
					currentBirdIndex++;
					AnimateBirdToSlingshot();
				}
			});
	}

	//Comprobate enemies
	bool AllEnemyDestroy()
	{
		return Pigs.All(x => x == null);
	}

	//Comprobate rigidbodies movements
	bool BricksBirdsPigsStoppedMoving()
	{
		foreach (var item in Bricks.Union(Birds).Union(Pigs))
		{
			if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.Min_Velocity)
			{
				return false;
			}
		}

		return true;
	}
}
