using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI numberOfDiscardedCards;
	
	private List<Card> discardedCards = new List<Card>();
	public IReadOnlyList<Card> DiscardedCards => discardedCards;

	public void AddDiscardedCard(Card discardedCard)
	{
		discardedCard.gameObject.SetActive(false);
		discardedCard.transform.SetParent(transform);

		StartCoroutine(DelayAddingToDiscarded(discardedCard));

	}

	private IEnumerator DelayAddingToDiscarded(Card discardedCard)
	{
		// due to the way the flow works, the card is first used, then discarded, and THEN weakened. So I use this hack
		// to make sure that the check for useability is made after the weakening
		yield return null;
		
		if (discardedCard.CardStats.IsCardUseable == true)
		{
			discardedCards.Add(discardedCard);
			numberOfDiscardedCards.text = discardedCards.Count.ToString();
		}
	}

	public IReadOnlyList<Card> RetrieveDiscardedCards()
	{
		var cards = new List<Card>(DiscardedCards);
		discardedCards.Clear();
		numberOfDiscardedCards.text = discardedCards.Count.ToString();
		
		return cards;
	}
}