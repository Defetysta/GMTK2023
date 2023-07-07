using System;
using UnityEngine;

public class CardsGenerator : MonoBehaviour
{
	[SerializeField]
	private CardBase[] defaultDeck;

	[SerializeField]
	private Card cardTemplate;

	private void Awake()
	{
		for (int i = 0; i < defaultDeck.Length; i++)
		{
			var newCard = Instantiate(cardTemplate, transform);
			newCard.SetCardStats(defaultDeck[i].GetStats());
		}
	}
}