using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private float timePerMove;
	[SerializeField]
	private float timeBeforeFirstMove;

	[SerializeField]
	private StatsDisplayer enemyStats;
	[SerializeField]
	private StatsDisplayer playerStats;

	[SerializeField]
	private PlayerHand playerHand;

	[SerializeField]
	private Enemy enemyCombatController;
	[SerializeField]
	private FighterStats playerCombatController;

	[SerializeField]
	private DiscardPile discardPile;

	[SerializeField]
	private Deck playerDeck;

	[SerializeField]
	private TextMeshProUGUI attackIntent;
	[SerializeField]
	private TextMeshProUGUI defendIntent;
	[SerializeField]
	private TextMeshProUGUI healIntent;

	public Button startTurnButton;

	private int enemyMoveCounter = 0;
	private Enemy enemyCombatControllerCopy;
	private FighterStats playerCombatControllerCopy;

	private List<CardStatsBase> enemyCardsThisTurn = new();
	private void Awake()
	{
		enemyCombatControllerCopy = Instantiate(enemyCombatController);
		enemyCombatControllerCopy.InitCopy();
		playerCombatControllerCopy = Instantiate(playerCombatController);
		enemyStats.Display(enemyCombatControllerCopy.Stats);
		playerStats.Display(playerCombatControllerCopy);
		
		playerDeck.Initialize(discardPile);
		
		startTurnButton.onClick.AddListener(StartTurn);
	}
	private void StartTurn()
	{
		StartCoroutine(PlayerTurn());
	}

	private IEnumerator PlayerTurn()
	{
		var enemyMoves = enemyCombatControllerCopy.Moveset.Moves;

		if (enemyMoveCounter >= enemyMoves.Length)
		{
			enemyMoveCounter = 0;
		}
		enemyCardsThisTurn.Clear();

		DisplayEnemyIntents(enemyMoves);

		int i = 0;

		playerHand.AddCards(playerDeck.DrawCards());

		yield return new WaitForSeconds(timeBeforeFirstMove);
		
		do
		{
			yield return new WaitForSeconds(timePerMove);
			var cardToApply = playerHand.CardSlots[i].AttachedCard;
			ApplyCard(cardToApply);

			if (cardToApply.WasSwappedIn == true)
			{
				cardToApply.CardStats.WeakenEffect();
				cardToApply.gameObject.name = "weakened_" + cardToApply.CardStats.EffectValue;
				cardToApply.SetWasSwappedIn(false);
			}
			
			i++;
			
		} while (i < playerHand.CardSlots.Length);

		HandleEnemyMoves();
		enemyStats.Display(enemyCombatControllerCopy.Stats);
		playerStats.Display(playerCombatControllerCopy);
	}

	private void DisplayEnemyIntents(Moveset.MovesGroup[] enemyMoves)
	{
		int totalEnemyDamage = 0;
		int totalEnemyArmor = 0;
		int totalEnemyHealing = 0;

		var actions = enemyMoves[enemyMoveCounter].Actions;
		for (int i = 0; i < actions.Length; i++)
		{
			enemyCardsThisTurn.Add(actions[i].GetStats());

			// var cards = enemyMoves[enemyMoveCounter].Actions;
			// for (int j = 0; j < cards.Length; j++)
			{
				// var cardStats =  cards[j].GetStats();
				var cardStats =  actions[i].GetStats();
				if (cardStats is CardSimpleAttack attackCard)
				{
					totalEnemyDamage += attackCard.EffectValue;
				}
				else if (cardStats is CardSimpleDefend defenseCard)
				{
					totalEnemyArmor += defenseCard.EffectValue;
				}
				else if (cardStats is CardSimpleHeal healCard)
				{
					totalEnemyHealing += healCard.EffectValue;
				}
			}
		}

		attackIntent.text = totalEnemyDamage.ToString();
		defendIntent.text = totalEnemyArmor.ToString();
		healIntent.text = totalEnemyHealing.ToString();
	}

	private void HandleEnemyMoves()
	{
		for (int j = 0; j < enemyCardsThisTurn.Count; j++)
		{
			var target = enemyCardsThisTurn[j].TargetEnemy ? playerCombatControllerCopy : enemyCombatControllerCopy.Stats;
			enemyCardsThisTurn[j].ApplyEffect(target);
		}

		attackIntent.text = String.Empty;
		defendIntent.text = String.Empty;
		healIntent.text = String.Empty;
		
		enemyMoveCounter++;
	}

	private void ApplyCard(Card usedCard)
	{
		var target = usedCard.CardStats.TargetEnemy ? enemyCombatControllerCopy.Stats : playerCombatControllerCopy;
		usedCard.CardStats.ApplyEffect(target);
		usedCard.Detach();
		
		enemyStats.Display(enemyCombatControllerCopy.Stats);
		playerStats.Display(playerCombatControllerCopy);
		discardPile.AddDiscardedCard(usedCard);
	}
}