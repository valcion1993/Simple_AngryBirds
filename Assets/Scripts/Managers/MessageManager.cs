using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
	public static MessageManager instance;

	[SerializeField] private Animator messageAnim;
	[SerializeField] private TextMeshProUGUI messageText;
	// Start is called before the first frame update
	void Awake()
	{
		instance = this;
	}

	public void ShowMessage(string message, float duration = 1)
	{
		messageAnim.SetTrigger("Show");

		messageText.text = message;

		StopAllCoroutines();
		StartCoroutine(HideMessage(duration));
	}

	IEnumerator HideMessage(float duration)
	{
		yield return new WaitForSeconds(duration);

		messageAnim.SetTrigger("Hide");
	}
}
