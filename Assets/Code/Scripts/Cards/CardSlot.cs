using System;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
	public static Action<CardSlot> CardSlotClicked;
	
	public Card attachedCard;

	public bool IsEmpty => attachedCard != null;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(() => CardSlotClicked.Invoke(this));
	}

	private void OnDestroy()
	{
		GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// public void Attach(Card targetCard)
	// {
	// 	attachedCard = targetCard;
	// 	attachedCard.MoveToTarget(transform);
	// }
	//
	// public void Swap(Card otherCard)
	// {
	// 	otherCard.HolderCardSlot.Attach(attachedCard);
	// 	Attach(otherCard);
	// }

	public void EmptySlot()
	{
		attachedCard = null;
	}

	// private void OnCardSlotClicked()
	// {
	// 	if (Card.CurrentlySelected == null)
	// 	{
	// 		Debug.Log("No card selected, early return");
	// 		
	// 		return;
	// 	}
	//
	// 	if (attachedCard != null)
	// 	{
	// 		Card.CurrentlySelected.Swap(attachedCard);
	// 	}
	// 	else
	// 	{
	// 		Card.CurrentlySelected.Attach(this);
	// 	}
	// 	
	// 	// Card.CurrentlySelected = null;
	// }
}
