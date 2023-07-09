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
	private EnemiesSequenceController enemiesSequenceController;
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

	[SerializeField]
	private GameOverHandler gameOverView;

	[SerializeField]
	private SimpleAudioEvent gameOverSound;

	public Button startTurnButton;

	private int enemyMoveCounter = 0;
	private Enemy currentEnemyCombatController;
	private FighterStats playerCombatControllerCopy;

	private List<CardStatsBase> enemyCardsThisTurn = new();
	private void Awake()
	{
		currentEnemyCombatController = enemiesSequenceController.GetNextEnemy();
		
		playerCombatControllerCopy = Instantiate(playerCombatController);
		enemyStats.Display(currentEnemyCombatController.Stats);
		playerStats.Display(playerCombatControllerCopy);
		
		playerDeck.Initialize(discardPile);
		playerDeck.PrepareDeck();
		
		startTurnButton.onClick.AddListener(StartTurn);
	}
	private void StartTurn()
	{
		StartCoroutine(PlayerTurn());
	}

	private IEnumerator PlayerTurn()
	{
		startTurnButton.gameObject.SetActive(false);
		var enemyMoves = currentEnemyCombatController.Moveset.Moves;

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
		
		playerDeck.CardsPanel.SetActive(false);

		if (currentEnemyCombatController.Stats.HP.Value <= 0)
		{
			// for (int j = 0; j < playerHand.CardSlots.Length; j++)
			// {
			// 	playerHand.CardSlots[j].AttachedCard?.Detach();
			// }
			
			currentEnemyCombatController = enemiesSequenceController.GetNextEnemy();
			discardPile.RetrieveDiscardedCards();

			playerDeck.PrepareDeck();

			if (currentEnemyCombatController == null)
			{
				Debug.Log("Victory!");
				
				yield break;
			}
		}

		HandleEnemyMoves();
		enemyStats.Display(currentEnemyCombatController.Stats);
		playerStats.Display(playerCombatControllerCopy);

		if (playerCombatControllerCopy.HP.Value <= 0)
		{
			gameOverView.gameObject.SetActive(true);
			// gameOverSound.Play();
		}

		// yield return StartCoroutine(PlayerTurn());
		startTurnButton.gameObject.SetActive(true);
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
			var target = enemyCardsThisTurn[j].TargetEnemy ? playerCombatControllerCopy : currentEnemyCombatController.Stats;
			enemyCardsThisTurn[j].ApplyEffect(target);
		}

		attackIntent.text = "?";
		defendIntent.text = "?";
		healIntent.text = "?";
		
		enemyMoveCounter++;
	}

	private void ApplyCard(Card usedCard)
	{
		var target = usedCard.CardStats.TargetEnemy ? currentEnemyCombatController.Stats : playerCombatControllerCopy;
		usedCard.CardStats.ApplyEffect(target);
		usedCard.Detach();
		
		enemyStats.Display(currentEnemyCombatController.Stats);
		playerStats.Display(playerCombatControllerCopy);
		discardPile.AddDiscardedCard(usedCard);
	}
}