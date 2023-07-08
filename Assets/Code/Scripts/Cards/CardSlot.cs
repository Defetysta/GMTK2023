using System;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
	public static Action<CardSlot> CardSlotClicked;
	
	public Card AttachedCard;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(() => CardSlotClicked?.Invoke(this));
	}

	private void OnDestroy()
	{
		GetComponent<Button>().onClick.RemoveAllListeners();
	}

}
