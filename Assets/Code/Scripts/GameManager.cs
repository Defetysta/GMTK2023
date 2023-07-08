using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Me;

	[SerializeField]
	private float timePerMove;

	[SerializeField]
	private StatsDisplayer enemyStats;
	[SerializeField]
	private StatsDisplayer playerStats;

	[SerializeField]
	private CardSlot[] cardSlots;

	[SerializeField]
	private FighterStats enemyCombatController;
	[SerializeField]
	private FighterStats playerCombatController;
	
	private void Awake()
	{
		if (Me == null)
		{
			Me = this;
		}

		enemyStats.Display(enemyCombatController);
		playerStats.Display(playerCombatController);
		// enemyCombatController = new FighterStats(enemyHP);
		// playerCombatController = new FighterStats(myHP);
	}

	[ContextMenu("xd")]
	public void xd()
	{
		StartCoroutine(PlayerTurn());
	}

	private IEnumerator PlayerTurn()
	{
		Debug.Log("start coroutine");
		int i = 0;
		do
		{
			Debug.Log("coroutine " + i);
			yield return new WaitForSeconds(timePerMove);
			ApplyCard(cardSlots[i].AttachedCard);
			i++;
			
		} while (i < cardSlots.Length);
	}

	private void ApplyCard(Card usedCard)
	{
		var target = usedCard.CardStats.TargetEnemy ? enemyCombatController : playerCombatController;
		usedCard.CardStats.ApplyEffect(target);
		
		enemyStats.Display(enemyCombatController);
		playerStats.Display(playerCombatController);
	}

	public void ApplyCards(IReadOnlyList<Card> usedCards)
	{
		for (int i = 0; i < usedCards.Count; i++)
		{
			var target = usedCards[i].CardStats.TargetEnemy ? enemyCombatController : playerCombatController;
			usedCards[i].CardStats.ApplyEffect(target);
		}
		
		enemyStats.Display(enemyCombatController);
		playerStats.Display(playerCombatController);
	}
}