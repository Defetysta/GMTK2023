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
		discardedCards.Add(discardedCard);
		discardedCard.gameObject.SetActive(false);
		discardedCard.transform.SetParent(transform);
		numberOfDiscardedCards.text = discardedCards.Count.ToString();
	}

	public IReadOnlyList<Card> RetrieveDiscardedCards()
	{
		var cards = new List<Card>(DiscardedCards);
		discardedCards.Clear();
		numberOfDiscardedCards.text = discardedCards.Count.ToString();
		
		Shuffle(cards);
		
		return cards;
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