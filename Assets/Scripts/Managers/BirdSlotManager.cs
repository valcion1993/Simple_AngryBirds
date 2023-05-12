using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSlotManager : MonoBehaviour
{
	public static BirdSlotManager instance;

	[HideInInspector] public SlotDrop[] Slots;

	// Start is called before the first frame update
	void Awake()
	{
		instance = this;

		Slots = GetComponentsInChildren<SlotDrop>();
	}

	public bool EquipedBird()
	{
		for (int i = 0; i < Slots.Length; i++)
		{
			if (Slots[i].equipedItem)
				return true;
		}

		return false;
	}
}
