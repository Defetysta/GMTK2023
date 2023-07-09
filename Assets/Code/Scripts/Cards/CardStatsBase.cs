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
	
	public abstract int EffectValue { get; }
	protected int modifier = 0;
	public string CardName => cardName;
	public Color EffectColor => effectColor;
	public bool TargetEnemy => targetEnemy;
	public bool IsCardUseable => EffectValue > 0;

	public abstract void ApplyEffect(FighterStats target);

	public abstract void WeakenEffect();

	public void ResetModifier()
	{
		modifier = 0;
	}
}