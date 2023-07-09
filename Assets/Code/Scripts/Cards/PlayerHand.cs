using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	[SerializeField]
	private CardSlot[] cardSlots;

	public CardSlot[] CardSlots => cardSlots;

	public void AddCards(List<Card> newCards)
	{
		for (int i = 0; i < cardSlots.Length; i++)
		{
			newCards[i].Attach(cardSlots[i]);
		}
	}
}