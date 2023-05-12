using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
	[SerializeField] private Animator floatTextAnim;
	[SerializeField] private TextMeshProUGUI floatText;
	[HideInInspector] public bool isPlaying;
	// Start is called before the first frame update
	void Start()
	{

	}

	public void ShowText(string message, float duration = 1)
	{
		isPlaying = true;

		floatTextAnim.SetTrigger("Show");

		floatText.text = message;

		StopAllCoroutines();
		StartCoroutine(HideText(duration));
	}

	IEnumerator HideText(float duration)
	{
		yield return new WaitForSeconds(duration);

		isPlaying = false;

		floatTextAnim.SetTrigger("Hide");
	}
}
