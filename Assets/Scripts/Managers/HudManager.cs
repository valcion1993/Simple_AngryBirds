using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
	public static HudManager instance;

	[Header("Screens")]
	public GameObject SelectionScreen;
	public GameObject GameOverScreen;

	void Awake()
	{
		instance = this;
	}

	//Buttons
	public void StartGame()
	{
		if (!BirdSlotManager.instance.EquipedBird())
		{
			MessageManager.instance.ShowMessage("You must equip at least one bird!", 1);
			return;
		}
		BirdSpawner.instance.SpawnBirds();

		GameManager.instance.GameStart();

		SelectionScreen.SetActive(false);

		MessageManager.instance.ShowMessage("Game Started!", 2);
	}

	public void RestartGame()
	{
		LevelLoadManager.instance.LoadLevel("game");
	}
}
