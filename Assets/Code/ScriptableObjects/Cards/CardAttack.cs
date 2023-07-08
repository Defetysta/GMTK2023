using UnityEngine;

[CreateAssetMenu(menuName="Cards/Attack", fileName = "Assets/ScriptableObjects/Cards/Attack")]
public class CardAttack : CardBase
{
	[SerializeField]
	private CardSimpleAttack stats;
	
	public CardStatsBase Stats => stats;
	public override CardStatsBase GetStats() => stats;
}