using UnityEngine;

[CreateAssetMenu(menuName="Cards/Heal", fileName = "Assets/ScriptableObjects/Cards/Heal")]
public class CardHeal : CardBase
{
	[SerializeField]
	private CardSimpleHeal stats;
	
	public CardStatsBase Stats => stats;
	public override CardStatsBase GetStats() => stats;
}