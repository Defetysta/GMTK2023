using System;
using UnityEngine;

[Serializable]
public class CardSimpleHeal : CardStatsBase
{
	[SerializeField]
	private int healValue;

	public override int EffectValue => healValue;

	public override void ApplyEffect(FighterStats target)
	{
		float finalHP = Mathf.Clamp(target.HP.Value + EffectValue, target.HP.Value + EffectValue, target.MaxHP);
		target.HP.Value = finalHP;
	}
}