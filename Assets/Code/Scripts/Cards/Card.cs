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
	public TextMeshProUGUI cardDefenseText;

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

	#region UI Logic
	
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
	
	#endregion
}
