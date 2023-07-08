using System;
using UnityEngine;

[Serializable]
public class CardSimpleHeal : CardStatsBase
{
	[SerializeField]
	private int healValue;

	public override int EffectValue => healValue;
}