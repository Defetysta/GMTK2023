using System;
using UnityEngine;

[Serializable]
public abstract class CardStatsBase
{
	[SerializeField]
	private string cardName;

	[SerializeField]
	private Color effectColor;

	[Tooltip("If unchecked, it will target self")]
	[SerializeField]
	private bool targetEnemy;
	
	public string CardName => cardName;
	public bool TargetEnemy => targetEnemy;

	public abstract int EffectValue { get; }
	public Color EffectColor => effectColor;
}