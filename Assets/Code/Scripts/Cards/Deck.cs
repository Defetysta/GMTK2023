using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
	private const int CARDS_ON_TURN_START = 3;
	
	[SerializeField]
	private Card cardTemplate;
	[SerializeField]
	private CardBase[] defaultDeck;
	[SerializeField]
	private Transform availableCardsContainer;
	[SerializeField]
	private TextMeshProUGUI numberOfAvailableCards;
	
	private List<Card> availableCards = new List<Card>();
	public List<Card> AvailableCards => availableCards;

	private CardsGenerator cardsGenerator;
	private DiscardPile discardPile;

	public void Initialize(DiscardPile discardPile)
	{
		this.discardPile = discardPile;
	}
	
	private void Awake()
	{
		Shuffle(defaultDeck);
		
		cardsGenerator = new CardsGenerator(cardTemplate, defaultDeck, availableCardsContainer);

		var newCards = cardsGenerator.InitializeDeck();
		availableCards.AddRange(newCards);
		
		RetrieveDiscardedCards();
		
		numberOfAvailableCards.text = availableCards.Count.ToString();
	}

	public List<Card> DrawCards(int cardsToDraw = CARDS_ON_TURN_START)
	{
		List<Card> toDraw = new List<Card>();
		for (int i = 0; i < cardsToDraw; i++)
		{
			if (availableCards.Count == 0)
			{
				RetrieveDiscardedCards();
			}

			Card card = availableCards.FirstOrDefault();
			toDraw.Add(card);
			availableCards.Remove(card);
		}
		
		numberOfAvailableCards.text = availableCards.Count.ToString();
		
		return toDraw;
	}

	private void RetrieveDiscardedCards()
	{
		availableCards.AddRange(discardPile.RetrieveDiscardedCards().ToList());

		for (int i = 0; i < availableCards.Count; i++)
		{
			AddCardToDeck(availableCards[i]);
		}

		Shuffle(availableCards);
	}

	private void AddCardToDeck(Card card)
	{
		card.transform.SetParent(availableCardsContainer);
		card.gameObject.SetActive(true);
	}

	private void Shuffle<T>(IList<T> list)  
	{  
		var rng = new System.Random();
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}