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

	private int enemyMoveCounter = 0;
	private Enemy enemyCombatControllerCopy;
	private FighterStats playerCombatControllerCopy;
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
		int i = 0;

		playerHand.AddCards(playerDeck.DrawCards());

		yield return new WaitForSeconds(timeBeforeFirstMove);
		
		do
		{
			if (playerCombatControllerCopy.IsInvulnerable == true)
			{
				playerCombatControllerCopy.Invulnerability--;
			}
			
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

	private void HandleEnemyMoves()
	{
		if (enemyCombatControllerCopy.Stats.IsInvulnerable == true)
		{
			enemyCombatControllerCopy.Stats.Invulnerability--;
		}
		
		var enemyMoves = enemyCombatControllerCopy.Moveset.Moves;

		if (enemyMoveCounter >= enemyMoves.Length)
		{
			enemyMoveCounter = 0;
		}

		for (int j = 0; j < enemyMoves[enemyMoveCounter].Actions.Length; j++)
		{
			var stats = enemyMoves[enemyMoveCounter].Actions[j].GetStats();
			var target = stats.TargetEnemy ? playerCombatControllerCopy : enemyCombatControllerCopy.Stats;
			stats.ApplyEffect(target);
		}

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