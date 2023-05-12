using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[Header("Item prefab")]
	public GameObject ItemPrefab;

	//Components
	private Image iconImage;

	//references
	private Vector3 startPoint;
	private Transform mainParent;
	private Transform containerParent;

	private void Start()
	{
		//EquipeItem();
		iconImage = GetComponent<Image>();

		mainParent = HudManager.instance.transform;
		containerParent = transform.parent;
	}

	//Drag Interfaces Methods
	public void OnBeginDrag(PointerEventData eventData)
	{
		startPoint = transform.position;

		iconImage.raycastTarget = false;

		transform.SetParent(mainParent);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.position = startPoint;
		iconImage.raycastTarget = true;

		transform.SetParent(containerParent);
	}
}
