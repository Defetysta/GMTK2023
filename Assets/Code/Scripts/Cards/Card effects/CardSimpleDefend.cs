using System;
using UnityEngine;

[Serializable]
public class CardSimpleDefend : CardStatsBase
{
	[SerializeField]
	private int defenseValue;

	public override int EffectValue => defenseValue - modifier;

	public override void ApplyEffect(FighterStats target)
	{
		target.Armor.Value += target.Posture.Value + EffectValue;
	}
	
	public override void WeakenEffect()
	{
		modifier++;
	}
	
}