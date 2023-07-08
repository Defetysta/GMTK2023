using System;
using UnityEngine;

[Serializable]
public class CardParalysisEffect : CardStatsBase
{
	[SerializeField]
	private int effectDuration;

	public override int EffectValue => effectDuration - modifier;

	public override void ApplyEffect(FighterStats target)
	{
		target.ParalysisDuration += Mathf.Clamp(EffectValue, 0, int.MaxValue);
	}

	public override void WeakenEffect()
	{
		modifier++;
	}
}