using UnityEngine;

[CreateAssetMenu(menuName="Cards/Paralysis", fileName = "Assets/ScriptableObjects/Cards/Paralysis")]
public class CardParalysis : CardBase
{
	[SerializeField]
	private CardParalysisEffect stats;
	
	public override CardStatsBase GetStats() => stats;
    
}