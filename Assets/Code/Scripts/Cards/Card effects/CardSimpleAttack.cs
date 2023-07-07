using System;
using UnityEngine;

[Serializable]
public class CardSimpleAttack : CardStatsBase
{
	[SerializeField]
	private int attackValue;
	
	public override int EffectValue => attackValue;

	public override void ApplyEffect(FighterStats target)
	{
		float finalDamage = EffectValue;
		if (target.Armor.Value > 0)
		{
			finalDamage -= target.Armor.Value;
			target.Armor.Value = Mathf.Clamp(target.Armor.Value, 0, target.Armor.Value - EffectValue);
		}

		if (finalDamage > 0)
		{
			target.HP.Value -= finalDamage;
		}
	}
}