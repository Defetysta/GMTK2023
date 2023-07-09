using System;
using UnityEngine;

[CreateAssetMenu(menuName="Variables/Float", fileName = "Assets/ScriptableObjects/Variables/FloatValue")]
public class FloatValue : ScriptableObject
{
	[SerializeField]
	private float defaultValue;

	private void OnEnable()
	{
		Value = defaultValue;
	}

	public float Value { get; set; }
}