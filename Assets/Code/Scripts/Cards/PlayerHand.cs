using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	[SerializeField]
	private CardSlot[] cardSlots;

	public CardSlot[] CardSlots => cardSlots;

	private List<Card> cardsOnHand = new List<Card>();

	public IReadOnlyList<Card> CardsOnHand => cardsOnHand;

	private DiscardPile discardPile;

	public void Initialize(DiscardPile discardPile)
	{
		this.discardPile = discardPile;
	}

	public void AddCards(List<Card> newCards)
	{
		cardsOnHand.AddRange(newCards);
	}

	public void EndTurn()
	{
		DiscardHand();
	}

	public void RemoveCard(Card card)
	{
		cardsOnHand.Remove(cardsOnHand.FirstOrDefault(x => x.GetInstanceID() == card.GetInstanceID()));
	}

	private void DiscardHand()
	{
		for (int i = 0; i < cardsOnHand.Count; i++)
		{
			discardPile.AddDiscardedCard(cardsOnHand[i]);
		}
		
		cardsOnHand.Clear();
	}
}