using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextManager : MonoBehaviour
{
	public static FloatingTextManager instance;

	private FloatingText[] floatingText;
	[SerializeField] private float textDuration = 0.5f;
	// Start is called before the first frame update
	void Awake()
	{
		instance = this;

		floatingText = GetComponentsInChildren<FloatingText>();
	}

	public void ShowFloatingText(string text, Vector3 pos)
	{
		if (string.IsNullOrEmpty(text) || text == "0")
			return;

		for (int i = 0; i < floatingText.Length; i++)
		{
			if (!floatingText[i].isPlaying)
			{
				floatingText[i].transform.position = pos;
				floatingText[i].ShowText(text, textDuration);
				break;
			}
		}
	}
}
