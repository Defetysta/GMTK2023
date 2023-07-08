using UnityEngine;

[CreateAssetMenu(menuName="Cards/Defend", fileName = "Assets/ScriptableObjects/Cards/Defend")]
public class CardDefend : CardBase
{
	[SerializeField]
	private CardSimpleDefend stats;
	
	public CardStatsBase Stats => stats;

	public override CardStatsBase GetStats() => stats;
}