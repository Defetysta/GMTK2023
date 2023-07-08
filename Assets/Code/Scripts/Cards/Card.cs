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

	public CardStatsBase CardStats;
	
	public TextMeshProUGUI cardName;
	public TextMeshProUGUI cardEffectText;
	
	public bool WasSwappedIn { get; private set; }

	public CardSlot HolderCardSlot => holderCardSlot;

	private void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener(ButtonClicked);
	}

	private void OnDestroy()
	{
		myButton.onClick.RemoveAllListeners();
	}

	private void OnEnable()
	{
		if (CardStats != null)
		{
			SetCardStats(CardStats);
		}
	}

	#region UI Logic
	
	private void ButtonClicked()
	{
		if (CurrentlySelected == null)
		{
			CurrentlySelected = this;
		}
		else
		{
			if (holderCardSlot != null && CurrentlySelected != this)
			{
				Swap(CurrentlySelected);
				CurrentlySelected = null;
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

		newCard.WasSwappedIn = true;
	}

	public void Attach(CardSlot cardSlot)
	{
		cardSlot.AttachedCard = this;
		holderCardSlot = cardSlot;
		MoveToTarget(cardSlot.transform);
	}

	public void Detach()
	{
		holderCardSlot.AttachedCard = null;
		holderCardSlot = null;
	}
	
	public void TryDetach()
	{
		if (holderCardSlot == null)
		{
			Debug.Log("No holder, early return");

			return;
		}
		
		holderCardSlot.AttachedCard = null;
		holderCardSlot = null;
	}

	#endregion

	#region In game logic

	public void SetCardStats(CardStatsBase stats)
	{
		CardStats = stats;
		cardName.text = CardStats.CardName;
		cardEffectText.text = CardStats.EffectValue.ToString();
		cardEffectText.color = CardStats.EffectColor;
	}

	public void SetWasSwappedIn(bool desiredValue)
	{
		WasSwappedIn = desiredValue;
	}
	
	#endregion
}
