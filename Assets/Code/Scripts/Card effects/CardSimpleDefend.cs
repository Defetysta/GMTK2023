using System;
using UnityEngine;

[Serializable]
public class CardSimpleDefend : CardStatsBase
{
	[SerializeField]
	private int defenseValue;

	public override int EffectValue => defenseValue;
}