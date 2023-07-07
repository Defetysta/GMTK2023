using System;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
	public Transform AvailableCardsContainer;
	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(Clicked);
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

		Card.CurrentlySelected.transform.SetParent(AvailableCardsContainer);
		Card.CurrentlySelected.Detach();
		Card.CurrentlySelected = null;
	}
}