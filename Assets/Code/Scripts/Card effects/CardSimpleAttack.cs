using System;
using UnityEngine;

[Serializable]
public class CardSimpleAttack : CardStatsBase
{
	[SerializeField]
	private int attackValue;
	
	public override int EffectValue => attackValue;
}