using System.Collections.Generic;
using UnityEngine;

public class CardsGenerator
{
	private CardBase[] DefaultDeck { get; }
	private Card CardTemplate { get; }
	
	private Transform AvailableCardsContainer { get; }

	public CardsGenerator(Card cardTemplate, CardBase[] defaultDeck, Transform newCardsParent)
	{
		DefaultDeck = defaultDeck;
		CardTemplate = cardTemplate;
		AvailableCardsContainer = newCardsParent;
	}
	
	public List<Card> InitializeDeck()
	{
		var cards = new List<Card>();
		for (int i = 0; i < DefaultDeck.Length; i++)
		{
			Card newCard = GenerateCard(DefaultDeck[i]);
			newCard.AvailableCardsContainer = AvailableCardsContainer;
			cards.Add(newCard);
		}

		return cards;
	}

	public Card GenerateCard(CardBase cardBase)
	{
		Card newCard = Object.Instantiate(CardTemplate);
		newCard.SetCardStats(cardBase.GetStats());

		return newCard;
	}
}