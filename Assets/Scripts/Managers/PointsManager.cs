using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
	public static PointsManager instance;

	[SerializeField] private int currentPoints;
	[SerializeField] private TextMeshProUGUI pointsText;

	// Start is called before the first frame update
	void Awake()
	{
		instance = this;
	}

	public void AddPoints(int pointsValue)
	{
		currentPoints += pointsValue;

		pointsText.text = currentPoints.ToString() + " Points";
	}
}
