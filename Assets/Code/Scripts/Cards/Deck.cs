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
		GetComponent<Button>().onClick.AddListener(Clicked);
		
		cardsGenerator = new CardsGenerator(cardTemplate, defaultDeck, availableCardsContainer);

		var newCards = cardsGenerator.InitializeDeck();
		availableCards.AddRange(newCards);
		numberOfAvailableCards.text = availableCards.Count.ToString();
	}

	public List<Card> DrawCards(int cardsToDraw = CARDS_ON_TURN_START)
	{
		List<Card> toDraw = new List<Card>();
		for (int i = 0; i < cardsToDraw; i++)
		{
			if (availableCards.Count == 0)
			{
				availableCards = discardPile.RetrieveDiscardedCards().ToList();
			}

			Card card = availableCards.FirstOrDefault();
			card.transform.SetParent(availableCardsContainer);
			card.gameObject.SetActive(true);
			toDraw.Add(card);
			availableCards.Remove(card);
		}
		
		numberOfAvailableCards.text = availableCards.Count.ToString();
		
		return toDraw;
	}

	public void Clicked()
	{
		if (Card.CurrentlySelected == null)
		{
			Debug.Log("No card selected, early return");
			
			return;
		}

		if (Card.CurrentlySelected.HolderCardSlot == null)
		{
			Debug.Log("Card doesn't have a holder; resetting currently selected; early return");
			Card.CurrentlySelected = null;
			
			return;
		}

		Card.CurrentlySelected.transform.SetParent(availableCardsContainer);
		Card.CurrentlySelected.Detach();
		Card.CurrentlySelected = null;
	}
}