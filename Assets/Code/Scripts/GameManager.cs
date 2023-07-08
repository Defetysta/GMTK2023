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
	private FighterStats enemyCombatController;
	[SerializeField]
	private FighterStats playerCombatController;

	[SerializeField]
	private DiscardPile discardPile;

	[SerializeField]
	private Deck playerDeck;

	public Button startTurnButton;
	public Button endTurnButton;
	
	private void Awake()
	{
		enemyStats.Display(enemyCombatController);
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
	}

	private void ApplyCard(Card usedCard)
	{
		var target = usedCard.CardStats.TargetEnemy ? enemyCombatController : playerCombatController;
		usedCard.CardStats.ApplyEffect(target);
		usedCard.Detach();
		
		enemyStats.Display(enemyCombatController);
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