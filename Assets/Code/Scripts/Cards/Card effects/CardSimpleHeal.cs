using System;
using UnityEngine;

[Serializable]
public class CardSimpleHeal : CardStatsBase
{
	[SerializeField]
	private int healValue;
	
	[SerializeField]
	private SimpleAudioEvent audioEvent;
	
	public override int EffectValue => healValue - modifier;

	public override void ApplyEffect(FighterStats target)
	{
		float finalHP = Mathf.Clamp(target.HP.Value + EffectValue, target.HP.Value + EffectValue, target.MaxHP);
		target.HP.Value = finalHP;
		
		audioEvent?.Play();
	}

	public override void WeakenEffect()
	{
		modifier++;
	}
}