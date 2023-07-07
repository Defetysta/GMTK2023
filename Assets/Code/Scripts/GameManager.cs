using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Me;

	[SerializeField]
	private FloatValue myHP;

	[SerializeField]
	private FloatValue enemyHP;

	private FighterStats enemyCombatController;
	private FighterStats myCombatController;
	
	private void Awake()
	{
		if (Me == null)
		{
			Me = this;
		}

		// enemyCombatController = new FighterStats(enemyHP);
		// myCombatController = new FighterStats(myHP);
	}

	public void ApplyCards(IReadOnlyList<Card> usedCards)
	{
		for (int i = 0; i < usedCards.Count; i++)
		{
			var target = usedCards[i].CardStats.TargetEnemy ? enemyCombatController : myCombatController;
			usedCards[i].CardStats.ApplyEffect(target);
		}
	}
}