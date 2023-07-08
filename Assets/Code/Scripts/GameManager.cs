using System.Collections;
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

	public Button startTurnButton;
	public Button endTurnButton;

	private int enemyMoveCounter = 0;
	private void Awake()
	{
		enemyStats.Display(enemyCombatController.Stats);
		playerStats.Display(playerCombatController);
		
		playerHand.Initialize(discardPile);
		playerDeck.Initialize(discardPile);
		
		startTurnButton.onClick.AddListener(StartTurn);
		endTurnButton.onClick.AddListener(EndTurn);
	}
	private void StartTurn()
	{
		StartCoroutine(PlayerTurn());
	}

	private IEnumerator PlayerTurn()
	{
		int i = 0;
		
		playerHand.AddCards(playerDeck.DrawCards());

		yield return new WaitForSeconds(timeBeforeFirstMove);
		
		do
		{
			yield return new WaitForSeconds(timePerMove);
			ApplyCard(playerHand.CardSlots[i].AttachedCard);
			i++;
			
		} while (i < playerHand.CardSlots.Length);
		
		playerHand.EndTurn();

		HandleEnemyMoves();
		enemyStats.Display(enemyCombatController.Stats);
		playerStats.Display(playerCombatController);
	}

	private void HandleEnemyMoves()
	{
		var enemyMoves = enemyCombatController.Moveset.Moves;

		if (enemyMoveCounter >= enemyMoves.Length)
		{
			enemyMoveCounter = 0;
		}

		for (int j = 0; j < enemyMoves[enemyMoveCounter].Actions.Length; j++)
		{
			var stats = enemyMoves[enemyMoveCounter].Actions[j].GetStats();
			var target = stats.TargetEnemy ? playerCombatController : enemyCombatController.Stats;
			stats.ApplyEffect(target);
		}

		enemyMoveCounter++;
	}

	private void ApplyCard(Card usedCard)
	{
		var target = usedCard.CardStats.TargetEnemy ? enemyCombatController.Stats : playerCombatController;
		usedCard.CardStats.ApplyEffect(target);
		usedCard.Detach();
		
		enemyStats.Display(enemyCombatController.Stats);
		playerStats.Display(playerCombatController);
		discardPile.AddDiscardedCard(usedCard);
		playerHand.RemoveCard(usedCard);
	}

	private void EndTurn()
	{
		playerHand.EndTurn();
		StopAllCoroutines();
	}
}