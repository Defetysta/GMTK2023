using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	public static Card CurrentlySelected;
	
	private Button myButton;
	private CardSlot holderCardSlot;
	public Transform AvailableCardsContainer;

	public CardSlot HolderCardSlot => holderCardSlot;

	private void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener(ButtonClicked);
		CardSlot.CardSlotClicked += CardSlotClicked;
	}

	private void OnDestroy()
	{
		myButton.onClick.RemoveAllListeners();
		CardSlot.CardSlotClicked -= CardSlotClicked;
	}

	private void CardSlotClicked(CardSlot targetSlot)
	{
		if (CurrentlySelected != this)
		{
			return;
		}
		
		Attach(targetSlot);
		CurrentlySelected = null;
	}
	
	private void ButtonClicked()
	{
		if (CurrentlySelected == null)
		{
			CurrentlySelected = this;
			Debug.Log(GetComponentInChildren<TextMeshProUGUI>().text);
		}
		else
		{
			if (holderCardSlot != null && CurrentlySelected != this)
			{
				// CurrentlySelected.HolderCardSlot.Swap(this);
				Swap(CurrentlySelected);
				CurrentlySelected = null;
				Debug.Log(GetComponentInChildren<TextMeshProUGUI>().text);
			}
		}
	}
	
	public void MoveToTarget(Transform target)
	{
		transform.SetParent(target);
		transform.localPosition = Vector3.zero;
	}

	public void Swap(Card newCard)
	{
		var targetHolder = newCard.HolderCardSlot;
		var currentHolder = HolderCardSlot;
		
		newCard.TryDetach();
		Detach();
		
		newCard.Attach(currentHolder);

		if (targetHolder != null)
		{
			Attach(targetHolder);
		}
		else
		{
			transform.SetParent(AvailableCardsContainer);
		}
	}

	public void Attach(CardSlot cardSlot)
	{
		cardSlot.attachedCard = this;
		holderCardSlot = cardSlot;
		MoveToTarget(cardSlot.transform);
	}

	public void Detach()
	{
		holderCardSlot.attachedCard = null;
		holderCardSlot = null;
	}
	
	public void TryDetach()
	{
		if (holderCardSlot == null)
		{
			Debug.Log("No holder, early return");

			return;
		}
		
		holderCardSlot.attachedCard = null;
		holderCardSlot = null;
	}
}
