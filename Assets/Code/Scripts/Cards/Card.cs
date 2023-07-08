using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	public static Card CurrentlySelected;

	[SerializeField]
	private SimpleAudioEvent moveCard;
	
	private Button myButton;
	private CardSlot holderCardSlot;
	public Transform AvailableCardsContainer;

	public CardStatsBase CardStats;
	
	public TextMeshProUGUI cardName;
	public TextMeshProUGUI cardEffectText;

	[SerializeField]
	private Image cardImage;
	
	public bool WasSwappedIn { get; private set; }

	public CardSlot HolderCardSlot => holderCardSlot;

	private Deck deck;
	
	public void Init(Deck deck)
	{
		this.deck = deck;
	}
	
	private void Awake()
	{
		myButton = GetComponentInChildren<Button>(true);
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
		deck.RemoveCardFromPool(newCard);
		deck.AddCardToPool(this);
	}

	public void Attach(CardSlot cardSlot)
	{
		cardSlot.AttachedCard = this;
		deck.StartCoroutine(DelayByFrameAndResetRota());
		holderCardSlot = cardSlot;
		MoveToTarget(cardSlot.transform);
		
		moveCard?.Play();
	}

	public void Detach()
	{
#if !UNITY_EDITOR
		if(holderCardSlot == null)
		{
			return;
		}
#endif
		holderCardSlot.AttachedCard = null;
		deck.StartCoroutine(DelayByFrameAndResetRota());
		holderCardSlot = null;
		
		moveCard?.Play();
	}
	
	public void TryDetach()
	{
		if (holderCardSlot == null)
		{
			Debug.Log("No holder, early return");

			return;
		}
		
		holderCardSlot.AttachedCard = null;
		deck.StartCoroutine(DelayByFrameAndResetRota());
		holderCardSlot = null;
	}

	private IEnumerator DelayByFrameAndResetRota()
	{
		yield return null;
		
		transform.localRotation = Quaternion.identity;
	}

	#endregion

	#region In game logic

	public void SetCardStats(CardStatsBase stats)
	{
		CardStats = stats;
		// cardName.text = CardStats.CardName;
		cardImage.sprite = stats?.CardSprite;
		cardEffectText.text = CardStats.EffectValue.ToString();
		cardEffectText.color = CardStats.EffectColor;
	}

	public void SetWasSwappedIn(bool desiredValue)
	{
		WasSwappedIn = desiredValue;
	}
	
	#endregion
}
