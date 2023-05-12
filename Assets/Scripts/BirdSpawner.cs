using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
	public static BirdSpawner instance;

	[Header("Birds spawn pos")]
	[SerializeField] private Transform[] birdsSpawnPos;
	private SlotDrop[] slots;
	// Start is called before the first frame update
	void Awake()
	{
		instance = this;
	}

	public void SpawnBirds()
	{
		slots = BirdSlotManager.instance.Slots;

		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].equipedItem)
			{
				GameObject newBird = Instantiate(slots[i].equipedItem.ItemPrefab, birdsSpawnPos[i].position, Quaternion.identity);

				GameManager.instance.AddBird(newBird);
			}
		}
	}
}
