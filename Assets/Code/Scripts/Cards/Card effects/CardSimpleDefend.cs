using System;
using UnityEngine;

[Serializable]
public class CardSimpleDefend : CardStatsBase
{
	[SerializeField]
	private int defenseValue;

	[SerializeField]
	private SimpleAudioEvent audioEvent;
	
	public override int EffectValue => defenseValue - modifier;

	public override void ApplyEffect(FighterStats target)
	{
		target.Armor.Value += target.Posture.Value + EffectValue;
		
		audioEvent?.Play();
	}
	
	public override void WeakenEffect()
	{
		modifier++;
	}
	
}