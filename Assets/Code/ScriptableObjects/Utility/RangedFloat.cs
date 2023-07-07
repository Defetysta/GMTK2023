using System;
using UnityEngine;

[Serializable]
public struct RangedFloat
{
	public static string MIN_VALUE_NAME => nameof(minValue);
	public static string MAX_VALUE_NAME => nameof(maxValue);
	
	[SerializeField] 
	private float minValue;
	[SerializeField] 
	private float maxValue;
	
	public float MinValue => minValue;
	public float MaxValue => maxValue;
}