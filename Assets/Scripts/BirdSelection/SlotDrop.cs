using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SlotDrop : MonoBehaviour, IDropHandler, IPointerClickHandler
{
	[Header("Slot settings")]
	[SerializeField] Image iconImage;
	[SerializeField] Sprite nullImage;

	[HideInInspector] public Item equipedItem;

	public void OnDrop(PointerEventData eventData)
	{
		Item item = eventData.pointerDrag.transform.GetComponent<Item>();

		if (item)
		{
			EquipeItem(item);
		}
	}

	public void EquipeItem(Item item)
	{
		equipedItem = item;

		iconImage.sprite = item.GetComponent<Image>().sprite;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		equipedItem = null;

		iconImage.sprite = nullImage;
	}
}
